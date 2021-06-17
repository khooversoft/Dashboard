using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.sdk;
using DashboardMgmt.Application;
using Microsoft.Extensions.Logging;

namespace DashboardMgmt.Services
{
    public class EngagementStore
    {
        public EngagementStore(DatabaseOption databaseOption, ILoggerFactory loggerFactory)
        {
            DashboardMgmtClientConfig config = new()
            {
                ConnectionString = databaseOption.ConnectionString,
                EnableCache = true
            };

            Client = new DashboardMgmtClient(config, loggerFactory.CreateLogger<DashboardMgmtClient>());
        }

        public DashboardMgmtClient Client { get; }
    }
}
