using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Extensions;
using Toolbox.Tools;

namespace Toolbox.Sql
{
    public class SqlViewBuilder
    {
        private readonly string _sqlObject;
        private readonly List<(string ColumnName, string Value)> _restrictList = new List<(string ColumnName, string Value)>();

        public SqlViewBuilder(string sqlObject)
        {
            sqlObject.VerifyNotEmpty(nameof(sqlObject));

            _sqlObject = sqlObject;
        }

        public SqlViewBuilder Restrict(string columnName, string? value)
        {
            columnName.VerifyNotEmpty(name: (columnName));

            if (value.IsEmpty()) return this;

            _restrictList.Add((columnName, value));
            return this;
        }

        public string Build()
        {
            string cmd = $"SELECT * FROM {_sqlObject}";

            if( _restrictList.Count > 0)
            {
                cmd += " WHERE ";
                cmd += string.Join(" AND ", _restrictList.Select(x => $"{x.ColumnName} = '{x.Value}'"));
            }

            return cmd;
        }
    }
}
