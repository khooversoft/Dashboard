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

        public static DashboardMgmtClient GetClient()
        {
            return _client ?? new DashboardMgmtClient(GetConnectionString(), GetLoggerFactory().CreateLogger<DashboardMgmtClient>());
        }
    }
}
