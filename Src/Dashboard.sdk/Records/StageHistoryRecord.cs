using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Toolbox.Sql.Extensions;

namespace Dashboard.sdk.Records
{
    public record StageHistoryRecord
    {
        public int StageHistoryId { get; init; }

        public int EngagementId { get; init; }

        public int StageId { get; init; }

        public DateTime? StartDate { get; init; }

        public DateTime? CompletedDate { get; init; }

        public static StageHistoryRecord Read(SqlDataReader reader)
        {
            return new StageHistoryRecord
            {
                StageHistoryId = reader.Get<int>(nameof(StageHistoryId)),
                EngagementId = reader.Get<int>(nameof(EngagementId)),
                StageId = reader.Get<int>(nameof(StageId)),
                StartDate = reader.Get<DateTime?>(nameof(StartDate)),
                CompletedDate = reader.Get<DateTime?>(nameof(CompletedDate)),
            };
        }
    }
}
