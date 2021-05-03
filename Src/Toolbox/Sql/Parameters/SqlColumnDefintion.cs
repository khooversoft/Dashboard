using System;
using System.Data;
using Microsoft.Data.SqlClient.Server;
using Toolbox.Tools;

namespace Toolbox.Sql.Parameters
{
    public class SqlColumnDefintion<T>
    {
        public SqlColumnDefintion(string columnName, SqlDbType sqlDbType, Func<T, object> getValue, int? dataSize = null)
        {
            columnName.VerifyNotEmpty(nameof(columnName));
            getValue.VerifyNotNull(nameof(getValue));

            ColumnName = columnName;
            SqlDbType = sqlDbType;
            GetValue = getValue;
            DataSize = dataSize;
        }

        public string ColumnName { get; }

        public SqlDbType SqlDbType { get; }

        public int? DataSize { get; }

        public Func<T, object> GetValue { get; }

        /// <summary>
        /// Get Sql Metadata for column definition
        /// </summary>
        /// <returns></returns>
        public SqlMetaData GetSqlMetaData()
        {
            switch (this.SqlDbType)
            {
                case SqlDbType.NVarChar:
                    return new SqlMetaData(this.ColumnName, this.SqlDbType, this.DataSize ?? SqlMetaData.Max);

                default:
                    return new SqlMetaData(this.ColumnName, this.SqlDbType);
            }
        }
    }
}