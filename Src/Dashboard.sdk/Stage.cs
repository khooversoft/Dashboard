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
    public class Stage
    {
        private readonly DashboardMgmtClientConfig _config;
        private readonly ILogger _logger;
        private readonly CacheObject<IReadOnlyList<StageRecord>> _listCache = new CacheObject<IReadOnlyList<StageRecord>>(TimeSpan.FromSeconds(30));

        public Stage(DashboardMgmtClientConfig config, ILogger logger)
        {
            config.VerifyNotNull(nameof(config)).Verify();
            logger.VerifyNotNull(nameof(logger));

            _config = config;
            _logger = logger;
        }

        public async Task Delete(string stage)
        {
            _listCache.Clear();

            await new SqlExec(_config.ConnectionString, _logger)
                .SetCommand("[App].[Delete-Stage]", CommandType.StoredProcedure)
                .AddParameter(nameof(Stage), stage)
                .ExecuteNonQuery();
        }

        public async Task<IReadOnlyList<StageRecord>> List(int? stageId = null, string? stage = null, bool noCache = false)
        {
            if (!noCache && _config.EnableCache && _listCache.TryGetValue(out IReadOnlyList<StageRecord> records)) return records;

            string cmd = new SqlViewBuilder("[App].[View-Stage]")
                .Restrict("[StageId]", stageId)
                .Restrict("[Stage]", stage)
                .Build();

            IReadOnlyList<StageRecord> list = await new SqlExec(_config.ConnectionString, _logger)
                .SetCommand(cmd, CommandType.Text)
                .Execute(StageRecord.Read);

            _listCache.Set(list);
            return list;
        }

        public async Task<int> Set(string stage, int orderNumber)
        {
            _listCache.Clear();

            IReadOnlyList<ReturnId> returnId = await new SqlExec(_config.ConnectionString, _logger)
                .SetCommand("[App].[Set-Stage]", CommandType.StoredProcedure)
                .AddParameter(nameof(stage), stage)
                .AddParameter(nameof(orderNumber), orderNumber)
                .Execute<ReturnId>(ReturnId.Read);

            return returnId.First().Id;
        }
    }
}
