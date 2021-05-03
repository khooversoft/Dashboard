using Microsoft.Data.SqlClient;
using Toolbox.Sql.Extensions;

namespace Dashboard.sdk.Records
{
    public record ReturnId
    {
        public int Id { get; init; }


        public static ReturnId Read(SqlDataReader reader)
        {
            return new ReturnId
            {
                Id = reader.Get<int>(nameof(Id)),
            };
        }
    }
}