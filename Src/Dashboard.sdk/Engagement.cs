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
    public class Engagement
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public Engagement(string _connectionString, ILogger logger)
        {
            this._connectionString = _connectionString;
            _logger = logger;
        }

        public async Task<int> Create(string provider, string description)
        {
            IReadOnlyList<ReturnId> returnId = await new SqlExec(_connectionString, _logger)
                .SetCommand("[App].[Create-Engagement]", CommandType.StoredProcedure)
                .AddParameter(nameof(provider), provider)
                .AddParameter(nameof(description), description)
                .Execute<ReturnId>(ReturnId.Read);

            return returnId.First().Id;
        }

        public async Task Delete(int engagementId)
        {
            await new SqlExec(_connectionString, _logger)
                .SetCommand("[App].[Delete-Engagement]", CommandType.StoredProcedure)
                .AddParameter(nameof(engagementId), engagementId)
                .ExecuteNonQuery();
        }

        public async Task<IReadOnlyList<EngagementRecord>> List()
        {
            return await new SqlExec(_connectionString, _logger)
                .SetCommand("SELECT * FROM [App].[View-Engagement]", CommandType.Text)
                .Execute(EngagementRecord.Read);
        }
    }
}
