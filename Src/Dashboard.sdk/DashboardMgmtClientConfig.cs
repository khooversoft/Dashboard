using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Tools;

namespace Dashboard.sdk
{
    public class DashboardMgmtClientConfig
    {
        public string ConnectionString { get; init; } = null!;

        public bool EnableCache { get; init; } = false;
    }

    public static class DashboardMgmtClientConfigExtensions
    {
        public static void Verify(this DashboardMgmtClientConfig subject)
        {
            subject.VerifyNotNull(nameof(subject));

            subject.ConnectionString.VerifyNotEmpty(nameof(subject.ConnectionString));
        }
    }
}
