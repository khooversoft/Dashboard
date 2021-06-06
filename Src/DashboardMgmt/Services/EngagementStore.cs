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
            Client = new DashboardMgmtClient(databaseOption.ConnectionString, loggerFactory.CreateLogger<DashboardMgmtClient>());
        }

        public DashboardMgmtClient Client { get; }
    }
}
