﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardMgmt.Application.Menu
{
    public interface IMenuItem
    {
        public bool Enabled { get; }
    }
}