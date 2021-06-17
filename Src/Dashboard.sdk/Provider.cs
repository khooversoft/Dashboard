using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.sdk.Records;
using Microsoft.Extensions.Logging;
using Toolbox;
using Toolbox.Sql;
using Toolbox.Tools;

namespace Dashboard.sdk
{
    public class Provider
    {
        private readonly DashboardMgmtClientConfig _config;
        private readonly ILogger _logger;
        private readonly CacheObject<IReadOnlyList<ProviderRecord>> _listCache = new CacheObject<IReadOnlyList<ProviderRecord>>(TimeSpan.FromSeconds(30));

        public Provider(DashboardMgmtClientConfig config, ILogger logger)
        {
            config.VerifyNotNull(nameof(config)).Verify();
            logger.VerifyNotNull(nameof(logger));

            _config = config;
            _logger = logger;
        }

        public async Task Delete(string provider)
        {
            _listCache.Clear();

            await new SqlExec(_config.ConnectionString, _logger)
                .SetCommand("[App].[Delete-Provider]", CommandType.StoredProcedure)
                .AddParameter(nameof(provider), provider)
                .ExecuteNonQuery();
        }

        public async Task<IReadOnlyList<ProviderRecord>> List(int? providerId = null, string? provider = null, bool noCache = false)
        {
            if (!noCache && _config.EnableCache && _listCache.TryGetValue(out IReadOnlyList<ProviderRecord> records)) return records;

            string cmd = new SqlViewBuilder("[App].[View-Provider]")
                .Restrict("[ProviderId]", providerId)
                .Restrict("[Provider]", provider)
                .Build();

            IReadOnlyList<ProviderRecord> list = await new SqlExec(_config.ConnectionString, _logger)
                .SetCommand(cmd, CommandType.Text)
                .Execute(ProviderRecord.Read);

            _listCache.Set(list);
            return list;
        }

        public async Task<int> Set(string provider, bool show)
        {
            _listCache.Clear();

            IReadOnlyList<ReturnId> returnId = await new SqlExec(_config.ConnectionString, _logger)
                .SetCommand("[App].[Set-Provider]", CommandType.StoredProcedure)
                .AddParameter(nameof(provider), provider)
                .AddParameter(nameof(show), show, true)
                .Execute<ReturnId>(ReturnId.Read);

            return returnId.First().Id;
        }
    }
}