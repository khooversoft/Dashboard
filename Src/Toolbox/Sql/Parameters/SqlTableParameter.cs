using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using Toolbox.Tools;

namespace Toolbox.Sql.Parameters
{
    [DebuggerDisplay("Name={Name}, TableTypeName={TableTypeName.ToString()}")]
    public class SqlTableParameter<T> : ISqlParameter
    {
        public SqlTableParameter(string parameterName, string tableTypeName)
        {
            parameterName.VerifyNotEmpty(nameof(parameterName));
            tableTypeName.VerifyNotEmpty(nameof(tableTypeName));

            Name = parameterName;
            TableTypeName = tableTypeName;
        }

        public string Name { get; }

        public string TableTypeName { get; }

        /// <summary>
        /// Column definitions
        /// </summary>
        public IList<SqlColumnDefintion<T>> ColumnDefinitions { get; } = new List<SqlColumnDefintion<T>>();

        /// <summary>
        /// List of values to write
        /// </summary>
        public IList<T> Items { get; } = new List<T>();

        /// <summary>
        /// Return metadata definition from table binding
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SqlMetaData> GetSqlMetadataDefinition()
        {
            foreach (var item in ColumnDefinitions)
            {
                yield return item.GetSqlMetaData();
            }
        }

        /// <summary>
        /// Create SqlParameter for this table type
        /// </summary>
        /// <returns>sql parameter</returns>
        public SqlParameter ToSqlParameter()
        {
            SqlParameter sqlParam = new SqlParameter(Name, ConstructSqlDataRecord(Items));
            sqlParam.SqlDbType = SqlDbType.Structured;
            sqlParam.TypeName = TableTypeName;
            return sqlParam;
        }

        /// <summary>
        /// Construct SQL metadata for table stored procedures
        /// </summary>
        /// <param name="dataItems">list of data items for table type</param>
        /// <returns>Metadata and data for table stored procedures</returns>
        private IEnumerable<SqlDataRecord> ConstructSqlDataRecord(IEnumerable<T> dataItems)
        {
            dataItems.VerifyNotNull(nameof(dataItems));

            List<SqlMetaData> metaDataList = new List<SqlMetaData>(GetSqlMetadataDefinition());

            foreach (var item in dataItems)
            {
                SqlDataRecord sdr = new SqlDataRecord(metaDataList.ToArray());

                int index = 0;
                foreach (var rowItem in ColumnDefinitions)
                {
                    sdr.SetValue(index++, rowItem.GetValue(item));
                }

                yield return sdr;
            }
        }
    }
}
