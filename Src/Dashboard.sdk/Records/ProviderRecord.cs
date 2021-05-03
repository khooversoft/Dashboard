using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Toolbox.Sql.Extensions;

namespace Dashboard.sdk.Records
{
    public record ProviderRecord
    {
        public int ProviderId { get; init; }

        public string Provider { get; init; } = null!;

        public bool Show { get; init; }


        public static ProviderRecord Read(SqlDataReader reader)
        {
            return new ProviderRecord
            {
                ProviderId = reader.Get<int>(nameof(ProviderId)),
                Provider = reader.Get<string>(nameof(Provider)),
                Show = reader.Get<bool>(nameof(Show)),
            };
        }
    }
}
