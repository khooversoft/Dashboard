using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.sdk.Records;
using Microsoft.Extensions.Logging;
using Toolbox;
using Toolbox.Sql;
using Toolbox.Tools;

namespace Dashboard.sdk
{
    public class StageHistory
    {
        private readonly DashboardMgmtClientConfig _config;
        private readonly ILogger _logger;
        private readonly CacheObject<IReadOnlyList<StageHistoryRecord>> _listCache = new CacheObject<IReadOnlyList<StageHistoryRecord>>(TimeSpan.FromSeconds(30));

        public StageHistory(DashboardMgmtClientConfig config, ILogger logger)
        {
            config.VerifyNotNull(nameof(config)).Verify();
            logger.VerifyNotNull(nameof(logger));
            _config = config;
            _logger = logger;
        }

        public async Task Delete(string provider, string stage)
        {
            _listCache.Clear();

            await new SqlExec(_config.ConnectionString, _logger)
                .AddParameter(nameof(provider), provider)
                .AddParameter(nameof(stage), stage)
                .SetCommand("[App].[Delete-StageHistory]", CommandType.StoredProcedure)
                .ExecuteNonQuery();
        }

        public async Task<IReadOnlyList<StageHistoryRecord>> List(int? stageHistoryId = null, string? provider = null, string? stage = null, bool noCache = false)
        {
            if (!noCache && _config.EnableCache && _listCache.TryGetValue(out IReadOnlyList<StageHistoryRecord> records)) return records;

            string cmd = new SqlViewBuilder("[App].[View-StageHistory]")
                .Restrict("[StageHistoryId]", stageHistoryId)
                .Restrict("[Provider]", provider)
                .Restrict("[Stage]", stage)
                .Build();

            IReadOnlyList<StageHistoryRecord> list = await new SqlExec(_config.ConnectionString, _logger)
                .SetCommand(cmd, CommandType.Text)
                .Execute(StageHistoryRecord.Read);

            _listCache.Set(list);
            return list;
        }

        public async Task Set(string provider, string stage, DateTime? startDate, DateTime? completedDate)
        {
            _listCache.Clear();

            await new SqlExec(_config.ConnectionString, _logger)
                .SetCommand("[App].[Set-StageHistory]", CommandType.StoredProcedure)
                .AddParameter(nameof(provider), provider)
                .AddParameter(nameof(stage), stage)
                .AddParameter(nameof(startDate), startDate?.Date, true)
                .AddParameter(nameof(completedDate), completedDate?.Date, true)
                .ExecuteNonQuery();
        }
    }
}
