using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardMgmt.Application.Menu;

namespace DashboardMgmt.Application.Menu
{
    public class MenuButton : IMenuItem
    {
        public MenuButton(string text, Func<Task> onClick, string iconName, bool enabled)
        {
            Text = text;
            OnClick = onClick;
            IconName = iconName;
            Enabled = enabled;
        }

        public string Text { get; }
        public Func<Task> OnClick { get; }
        public string IconName { get; }
        public bool Enabled { get; }

    }
}
