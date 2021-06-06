using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolbox.Tools;

namespace DashboardMgmt.Application
{
    public class DatabaseOption
    {
        public string ConnectionString { get; set; }
    }

    public static class DatabaseOptionExtensions
    {
        public static void Verify(this DatabaseOption subject)
        {
            subject.VerifyNotNull(nameof(subject));

            subject.ConnectionString.VerifyNotEmpty(nameof(subject.ConnectionString));
        }
    }
}
