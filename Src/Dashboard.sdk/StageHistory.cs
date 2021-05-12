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

namespace Dashboard.sdk
{
    public class StageHistory
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public StageHistory(string _connectionString, ILogger logger)
        {
            this._connectionString = _connectionString;
            _logger = logger;
        }

        public async Task Delete(string provider, string stage) => await new SqlExec(_connectionString, _logger)
            .AddParameter(nameof(provider), provider)
            .AddParameter(nameof(stage), stage)
            .SetCommand("[App].[Delete-StageHistory]", CommandType.StoredProcedure)
            .ExecuteNonQuery();

        public async Task<IReadOnlyList<StageHistoryRecord>> List(string? provider = null, string? stage = null)
        {
            string cmd = new SqlViewBuilder("[App].[View-StageHistory]")
                .Restrict("[Provider]", provider)
                .Restrict("[Stage]", stage)
                .Build();

            return await new SqlExec(_connectionString, _logger)
                .SetCommand(cmd, CommandType.Text)
                .Execute(StageHistoryRecord.Read);
        }

        public async Task Set(string provider, string stage, DateTime? startDate, DateTime? completedDate) => await new SqlExec(_connectionString, _logger)
            .SetCommand("[App].[Set-StageHistory]", CommandType.StoredProcedure)
            .AddParameter(nameof(provider), provider)
            .AddParameter(nameof(stage), stage)
            .AddParameter(nameof(startDate), startDate?.Date, true)
            .AddParameter(nameof(completedDate), completedDate?.Date, true)
            .ExecuteNonQuery();
    }
}
