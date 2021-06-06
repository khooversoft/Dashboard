using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardMgnt.Server.Application
{
    public static class Constants
    {
        public static string KeyVaultName { get; } = nameof(KeyVaultName);

        public static string SqlConnectionSecretName { get; } = nameof(SqlConnectionSecretName);

        public static string SqlConnectionString { get; } = nameof(SqlConnectionString);
    }
}
