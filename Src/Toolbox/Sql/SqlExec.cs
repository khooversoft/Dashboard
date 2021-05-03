using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Toolbox.Sql.Parameters;
using Toolbox.Tools;
using Microsoft.Extensions.Logging;
using Toolbox.Sql.Extensions;

namespace Toolbox
{
    /// <summary>
    /// <para>SQL Execute, primary abstraction for ADO.NET with strong contracts</para>
    /// <para>If a deadlock is detected, the SQL command will be retried n times with a random back off delay between 10 and 1000 ms.</para>
    /// </summary>
    public class SqlExec
    {
        private static readonly Random _random = new Random();
        private readonly string _sqlConnectionString;
        private readonly ILogger _logger;
        private const int _retryCount = 5;
        private const int _deadLockNumber = 1205;
        private const string _deadLockMessage = "Deadlock retry failed";

        public SqlExec(string SqlConnectionString, ILogger logger)
        {
            SqlConnectionString.VerifyNotEmpty(nameof(SqlConnectionString));

            _sqlConnectionString = SqlConnectionString;
            _logger = logger;
        }

        /// <summary>
        /// SQL command
        /// </summary>
        public string Command { get; private set; } = null!;

        /// <summary>
        /// SQL command type
        /// </summary>
        public CommandType CommandType { get; private set; } = CommandType.StoredProcedure;

        /// <summary>
        /// Parameters
        /// </summary>
        public IList<ISqlParameter> Parameters { get; } = new List<ISqlParameter>();

        /// <summary>
        /// Set SQL command
        /// </summary>
        /// <param name="command">SQL command to use</param>
        /// <returns>this</returns>
        public SqlExec SetCommand(string command, CommandType commandType = CommandType.StoredProcedure)
        {
            command.VerifyNotEmpty(nameof(command));

            Command = command;
            CommandType = commandType;

            return this;
        }

        /// <summary>
        /// Set parameter
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="name">parameter name</param>
        /// <param name="value">value</param>
        /// <param name="addValueIfNull">Add value if null</param>
        /// <returns>this</returns>
        public SqlExec AddParameter<T>(string name, T? value, bool addValueIfNull = false)
        {
            name.VerifyNotEmpty(nameof(name));

            if (!addValueIfNull && value?.Equals(default(T)) == true)
            {
                return this;
            }

            if (typeof(T).IsEnum)
            {
                Parameters.Add(new SqlSimpleParameter(name, value!.ToString()!));
                return this;
            }


            Parameters.Add(new SqlSimpleParameter(name, value!));
            return this;
        }

        /// <summary>
        /// Add parameter
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public SqlExec AddParameter(ISqlParameter parameter)
        {
            parameter.VerifyNotNull(nameof(parameter));

            Parameters.Add(parameter);
            return this;
        }

        /// <summary>
        /// Execute None SQL query (no response)
        /// </summary>
        /// <param name="context">execution context</param>
        /// <returns>task</returns>
        public async Task ExecuteNonQuery()
        {
            SqlException? saveEx = null;

            using var conn = new SqlConnection(_sqlConnectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = Command;
            cmd.CommandType = CommandType;
            cmd.Parameters.AddRange(Parameters.Select(x => x.ToSqlParameter()).ToArray());

            conn.Open();

            for (int retry = 0; retry < _retryCount; retry++)
            {
                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Number == _deadLockNumber)
                    {
                        saveEx = sqlEx;
                        Thread.Sleep(TimeSpan.FromMilliseconds(_random.Next(10, 1000)));
                        continue;
                    }

                    _logger.LogError(saveEx, "Failed to execute sql command");
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to execute sql command");
                    throw;
                }
            }
        }

        /// <summary>
        /// Execute SQL and return data set
        /// </summary>
        /// <typeparam name="T">type to return</typeparam>
        /// <param name="context">execution context</param>
        /// <param name="factory">type factor</param>
        /// <returns>list of types</returns>
        public async Task<IReadOnlyList<T>> Execute<T>(Func<SqlDataReader, T> factory)
        {
            factory.VerifyNotNull(nameof(factory));

            return await ExecuteBatch(r => r.GetCollection(factory));
        }

        /// <summary>
        /// Execute SQL and batch object
        /// </summary>
        /// <typeparam name="T">type to return</typeparam>
        /// <param name="context">execution context</param>
        /// <param name="factory">batch type factor</param>
        /// <returns>list of types</returns>
        public async Task<T> ExecuteBatch<T>(Func<SqlDataReader, T> factory)
        {
            factory.VerifyNotNull(nameof(factory));

            SqlException? saveEx = null;

            using var conn = new SqlConnection(_sqlConnectionString);
            using var cmd = conn.CreateCommand();

            cmd.CommandText = Command;
            cmd.CommandType = CommandType;
            cmd.Parameters.AddRange(Parameters.Select(x => x.ToSqlParameter()).ToArray());

            conn.Open();

            for (int retry = 0; retry < _retryCount; retry++)
            {
                try
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        return factory(reader);
                    }
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Number == _deadLockNumber)
                    {
                        saveEx = sqlEx;
                        await Task.Delay(TimeSpan.FromMilliseconds(_random.Next(10, 1000)));
                        continue;
                    }

                    _logger.LogError(saveEx, "Failed to execute sql command");
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to execute sql command");
                    throw;
                }
            }

            throw new Exception(_deadLockMessage, saveEx);
        }
    }
}
