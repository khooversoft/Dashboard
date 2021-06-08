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
        private readonly List<(string ColumnName, object Value)> _restrictList = new List<(string ColumnName, object Value)>();

        public SqlViewBuilder(string sqlObject)
        {
            sqlObject.VerifyNotEmpty(nameof(sqlObject));

            _sqlObject = sqlObject;
        }

        public SqlViewBuilder Restrict<T>(string columnName, T? value)
        {
            columnName.VerifyNotEmpty(name: (columnName));

            switch(value)
            {
                case null: return this;

                case string v when v.IsEmpty(): return this;
            }

            _restrictList.Add((columnName, value));
            return this;
        }

        public string Build()
        {
            string cmd = $"SELECT * FROM {_sqlObject}";

            if( _restrictList.Count > 0)
            {
                cmd += " WHERE ";
                cmd += string.Join(" AND ", _restrictList.Select(x => $"{x.ColumnName} = {GetValue(x.Value)}"));
            }

            return cmd;
        }

        private string GetValue(object subject) => subject switch
        {
            null => "NULL",
            string v => $"'{v}'",
            DateTime v => $"'{v.ToShortDateString()}'",

            _ => subject?.ToString() ?? string.Empty,
        };
    }
}
