﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Models;
using TaskTrackerUI.Services;

namespace TaskTrackerUI.ViewModels
{
    public class ProjectVm : VMBase
    {

        public override async Task<bool> LoadData() {
            return true;  
        }
    }
}