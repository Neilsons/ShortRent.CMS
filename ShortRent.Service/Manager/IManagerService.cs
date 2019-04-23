﻿using ShortRent.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Service
{
    public interface IManagerService
    {
        List<Manager> GetManagers();
        List<Manager> GetTreeViewManagers();
        void CreateManager(Manager manager);
        Manager GetManager(int? id);
        void UpdateManager(Manager model);
    }
}