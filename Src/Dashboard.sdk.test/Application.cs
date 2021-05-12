using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Toolbox.Extensions;

namespace Dashboard.sdk.test
{
    public static class Application
    {
        private static string? _connectionString;
        private static ILoggerFactory? _loggerFactory;
        private static DashboardMgmtClient? _client = null;

        public static string GetConnectionString()
        {
            if (!_connectionString.IsEmpty()) return _connectionString!;

            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "DashboardMgnt",
                IntegratedSecurity = true,
            };

            return _connectionString = builder.ConnectionString;
        }

        public static ILoggerFactory GetLoggerFactory()
        {
            return _loggerFactory ??= LoggerFactory.Create(logger =>
            {
                logger.AddDebug();
            });
        }

        public static DashboardMgmtClient GetClient() => _client ?? new DashboardMgmtClient(GetConnectionString(), GetLoggerFactory().CreateLogger<DashboardMgmtClient>());

        public static async Task ClearDatabase()
        {
            await ClearStageHistory();
            await ClearProviders();
            await ClearStages();
        }

        public static async Task ClearStageHistory()
        {
            DashboardMgmtClient client = GetClient();
            var list = await client.StageHistory.List();
            await list.ForEachAsync(async x => await client.StageHistory.Delete(x.Provider, x.Stage));
        }

        public static async Task ClearProviders()
        {
            DashboardMgmtClient client = GetClient();
            var list = await client.Provider.List();
            await list.ForEachAsync(async x => await client.Provider.Delete(x.Provider));
        }

        public static async Task ClearStages()
        {
            DashboardMgmtClient client = GetClient();

            var list = await client.Stage.List();
            await list.ForEachAsync(async x => await client.Stage.Delete(x.Stage));
        }
    }
}
