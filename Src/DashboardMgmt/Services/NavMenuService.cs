using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardMgmt.Application;
using DashboardMgmt.Application.Menu;

namespace DashboardMgmt.Services
{
    public class NavMenuService
    {
        public IReadOnlyList<MenuItem> GetLeftMenuItems() => new[]
        {
            new MenuItem("Home", string.Empty, "oi-home", true),

            new MenuItem("Engagement", "oi-layers", Constants.Pages.Current, new MenuItem[]
            {
                new MenuItem("Current", Constants.Pages.Current, "oi-list-rich", true),
                new MenuItem("Provider", Constants.Pages.Provider, "oi-list-rich", true),
                new MenuItem("Stage", Constants.Pages.Stage, "oi-list-rich", true),
                //new MenuItem("History", Constants.Pages.Current, "oi-list-rich", true),
            }),

            new MenuItem("Dashboard", Constants.Pages.Batch, "oi-fork", true),
        };
    }
}
