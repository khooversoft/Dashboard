using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Toolbox.Sql.Extensions;

namespace Dashboard.sdk.Records
{
    public record EngagementRecord
    {
        public int EngagementId { get; init; }

        public int ProviderId { get; init; }

        public string? Description { get; init; }


        public static EngagementRecord Read(SqlDataReader reader)
        {
            return new EngagementRecord
            {
                EngagementId = reader.Get<int>(nameof(EngagementId)),
                ProviderId = reader.Get<int>(nameof(ProviderId)),
                Description = reader.Get<string>(nameof(Description))
            };
        }
    }
}
