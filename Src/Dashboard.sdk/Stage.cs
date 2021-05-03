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
    public class Stage
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public Stage(string _connectionString, ILogger logger)
        {
            this._connectionString = _connectionString;
            _logger = logger;
        }

        public async Task<int> Set(string stage, int orderNumber)
        {
            IReadOnlyList<ReturnId> returnId = await new SqlExec(_connectionString, _logger)
                .SetCommand("[App].[Set-Stage]", CommandType.StoredProcedure)
                .AddParameter(nameof(Stage), stage)
                .AddParameter(nameof(orderNumber), orderNumber)
                .Execute<ReturnId>(ReturnId.Read);

            return returnId.First().Id;
        }

        public async Task Delete(string stage)
        {
            await new SqlExec(_connectionString, _logger)
                .SetCommand("[App].[Delete-Stage]", CommandType.StoredProcedure)
                .AddParameter(nameof(Stage), stage)
                .ExecuteNonQuery();
        }

        public async Task<IReadOnlyList<StageRecord>> List()
        {
            return await new SqlExec(_connectionString, _logger)
                .SetCommand("SELECT * FROM [App].[View-Stage]", CommandType.Text)
                .Execute(StageRecord.Read);
        }
    }
}
