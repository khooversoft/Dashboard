using System;
using Microsoft.Data.SqlClient;
using Toolbox.Sql.Extensions;

namespace Dashboard.sdk.Records
{
    public record StageHistoryRecord
    {
        public int StageHistoryId { get; init; }

        public int ProviderId { get; init; }

        public string Provider { get; init; } = null!;

        public int StageId { get; init; }

        public string Stage { get; init; } = null!;

        public DateTime? StartDate { get; init; }

        public DateTime? CompletedDate { get; init; }

        public static StageHistoryRecord Read(SqlDataReader reader)
        {
            return new StageHistoryRecord
            {
                StageHistoryId = reader.Get<int>(nameof(StageHistoryId)),
                ProviderId = reader.Get<int>(nameof(ProviderId)),
                Provider = reader.Get<string>(nameof(Provider)),
                StageId = reader.Get<int>(nameof(StageId)),
                Stage = reader.Get<string>(nameof(Stage)),
                StartDate = reader.Get<DateTime?>(nameof(StartDate)),
                CompletedDate = reader.Get<DateTime?>(nameof(CompletedDate)),
            };
        }
    }
}