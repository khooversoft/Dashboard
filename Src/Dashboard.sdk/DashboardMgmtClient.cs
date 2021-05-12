using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Toolbox.Tools;

namespace Dashboard.sdk
{
    public class DashboardMgmtClient
    {
        private readonly string _connectionString;
        private readonly ILogger<DashboardMgmtClient> _logger;

        public DashboardMgmtClient(string connectionString, ILogger<DashboardMgmtClient> logger)
        {
            connectionString.VerifyNotEmpty(nameof(connectionString));
            logger.VerifyNotNull(nameof(logger));

            _connectionString = connectionString;
            _logger = logger;

            Provider = new Provider(_connectionString, logger);
            Stage = new Stage(_connectionString, logger);
            StageHistory = new StageHistory(_connectionString, logger);
        }

        public Provider Provider { get; }

        public Stage Stage { get; }

        public StageHistory StageHistory { get; }
    }
}
