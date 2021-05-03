using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Toolbox.Sql.Extensions;

namespace Dashboard.sdk.Records
{
    public record StageRecord
    {
        public int StageId { get; init; }

        public string Stage { get; init; } = null!;

        public int OrderNumber { get; init; }

        public static StageRecord Read(SqlDataReader reader)
        {
            return new StageRecord
            {
                StageId = reader.Get<int>(nameof(StageId)),
                Stage = reader.Get<string>(nameof(Stage)),
                OrderNumber = reader.Get<int>(nameof(OrderNumber)),
            };
        }
    }
}
