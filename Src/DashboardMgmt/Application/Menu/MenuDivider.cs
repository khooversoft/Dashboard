using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardMgmt.Application.Menu;

namespace DashboardMgmt.Application.Menu
{
    public class MenuDivider : IMenuItem
    {
        public bool Enabled => true;
    }
}
