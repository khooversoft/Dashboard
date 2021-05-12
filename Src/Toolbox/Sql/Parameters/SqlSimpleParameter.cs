using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Toolbox.Tools;

namespace Toolbox.Sql.Parameters
{
    /// <summary>
    /// Handle simple SQL parameters (immutable)
    /// </summary>
    [DebuggerDisplay("Name={Name}, Value={Value}")]
    public class SqlSimpleParameter : ISqlParameter
    {
        public SqlSimpleParameter(string name, object? value)
        {
            name.VerifyNotEmpty(nameof(name));

            Name = name;
            Value = value;
        }

        /// <summary>
        /// Name of the parameter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the parameter
        /// </summary>
        public object? Value { get; set; }

        /// <summary>
        /// Convert to SQL Parameter
        /// </summary>
        /// <returns>SQL parameter</returns>
        public SqlParameter ToSqlParameter()
        {
            return Value switch
            {
                null => new SqlParameter(Name, DBNull.Value),

                _ => new SqlParameter(Name, Value),
            };

            //if (Value == null) return new SqlParameter(Name, DBNull.Value);
            //return new SqlParameter(Name, Value);
        }
    }
}