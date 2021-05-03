using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.sdk.Records;
using Microsoft.Extensions.Logging;
using Toolbox;

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

        public async Task<int> Set(int stageHistoryId, DateTime? startDate, DateTime? completedDate)
        {
            IReadOnlyList<ReturnId> returnId = await new SqlExec(_connectionString, _logger)
                .SetCommand("[App].[Set-StageHistory]", CommandType.StoredProcedure)
                .AddParameter(nameof(stageHistoryId), stageHistoryId)
                .AddParameter(nameof(startDate), startDate)
                .AddParameter(nameof(completedDate), completedDate)
                .Execute<ReturnId>(ReturnId.Read);

            return returnId.First().Id;
        }

        public async Task<IReadOnlyList<ProviderRecord>> List()
        {
            return await new SqlExec(_connectionString, _logger)
                .SetCommand("SELECT * FROM [App].[View-StageHistory]", CommandType.Text)
                .Execute(ProviderRecord.Read);
        }
    }
}
