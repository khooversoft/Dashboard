using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardMgnt.Client.Application;
using DashboardMgnt.Client.Application.Menu;

namespace DashboardMgnt.Client.Services
{
    public class NavMenuService
    {
        public IReadOnlyList<MenuItem> GetLeftMenuItems() => new[]
        {
            new MenuItem("Home", string.Empty, "oi-home", true),

            new MenuItem("Engagement", "oi-layers", Constants.Pages.Current, new MenuItem[]
            {
                new MenuItem("Current", Constants.Pages.Current, "oi-list-rich", true),
                new MenuItem("Provider", Constants.Pages.Current, "oi-list-rich", true),
                new MenuItem("Stage", Constants.Pages.Current, "oi-list-rich", true),
                new MenuItem("History", Constants.Pages.Current, "oi-list-rich", true),
            }),

            new MenuItem("Dashboard", Constants.Pages.Batch, "oi-fork", true),
        };
    }
}
