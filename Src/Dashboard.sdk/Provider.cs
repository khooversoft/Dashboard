using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.sdk.Records;
using Microsoft.Extensions.Logging;
using Toolbox;
using Toolbox.Extensions;
using Toolbox.Sql;

namespace Dashboard.sdk
{
    public class Provider
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public Provider(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task Delete(string provider) => await new SqlExec(_connectionString, _logger)
            .SetCommand("[App].[Delete-Provider]", CommandType.StoredProcedure)
            .AddParameter(nameof(provider), provider)
            .ExecuteNonQuery();

        public async Task<IReadOnlyList<ProviderRecord>> List(int? providerId = null, string? provider = null)
        {
            string cmd = new SqlViewBuilder("[App].[View-Provider]")
                .Restrict("[ProviderId]", providerId)
                .Restrict("[Provider]", provider)
                .Build();

            return await new SqlExec(_connectionString, _logger)
                .SetCommand(cmd, CommandType.Text)
                .Execute(ProviderRecord.Read);
        }

        public async Task<int> Set(string provider, bool show)
        {
            IReadOnlyList<ReturnId> returnId = await new SqlExec(_connectionString, _logger)
                .SetCommand("[App].[Set-Provider]", CommandType.StoredProcedure)
                .AddParameter(nameof(provider), provider)
                .AddParameter(nameof(show), show, true)
                .Execute<ReturnId>(ReturnId.Read);

            return returnId.First().Id;
        }
    }
}
