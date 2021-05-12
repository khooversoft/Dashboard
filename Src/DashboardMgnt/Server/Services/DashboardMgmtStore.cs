using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.sdk;
using DashboardMgnt.Server.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DashboardMgnt.Server.Services
{
    public class DashboardMgmtStore
    {
        public DashboardMgmtStore(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            string connectionString = configuration[Constants.SqlConnectionSecretName];

            Client = new DashboardMgmtClient(connectionString, loggerFactory.CreateLogger<DashboardMgmtClient>());
        }

        public DashboardMgmtClient Client { get; }
    }
}
