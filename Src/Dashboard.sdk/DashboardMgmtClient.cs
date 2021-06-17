using Microsoft.Extensions.Logging;
using Toolbox.Tools;

namespace Dashboard.sdk
{
    public class DashboardMgmtClient
    {
        public DashboardMgmtClient(DashboardMgmtClientConfig config, ILogger<DashboardMgmtClient> logger, bool enableCache = false)
        {
            config.VerifyNotNull(nameof(config));
            logger.VerifyNotNull(nameof(logger));

            Provider = new Provider(config, logger);
            Stage = new Stage(config, logger);
            StageHistory = new StageHistory(config, logger);
        }

        public Provider Provider { get; }

        public Stage Stage { get; }

        public StageHistory StageHistory { get; }
    }
}