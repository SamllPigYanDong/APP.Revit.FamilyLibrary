﻿using Prism.Regions;
using Revit.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using Prism.Ioc;
using Revit.Shared.Base;
using Revit.Shared;

namespace Revit.Application.Views.ProjectViews
{
    /// <summary>
    /// ProjectView.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectView 
    {
        public ProjectView()
        {
            InitializeComponent(); 
            RegionManager.SetRegionManager(this, SharedModule.Instance.Container.Resolve<IRegionManager>());
            RegionManager.UpdateRegions();
        }
    }
}
