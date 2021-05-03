using Microsoft.Data.SqlClient;

namespace Toolbox.Sql.Parameters
{
    public interface ISqlParameter
    {
        SqlParameter ToSqlParameter();
    }
}
