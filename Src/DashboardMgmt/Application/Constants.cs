using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardMgmt.Application
{
    public static class Constants
    {
        public const string StartFocusId = "start-focus";
        public const string ExistFocusId = "exist-focus";

        public static class Pages
        {
            public static string Current { get; } = "current";
            public static string Provider { get; } = "provider";
            public static string Stage { get; } = "stage";
            public static string History { get; } = "history";
            public static string Batch { get; } = "batch";
        }
    }
}
