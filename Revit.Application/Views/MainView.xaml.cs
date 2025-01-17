﻿using System;
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
using System.Windows.Shapes;
using Wpf.Ui.Appearance;
using Prism.Ioc;
using Revit.Application.ViewModels;
using Prism.Regions;
using Wpf.Ui.Mvvm.Services;

namespace Revit.Application.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView:Window
    {
        private readonly IRegionManager regionManager;

        public MainView()
        {
            InitializeComponent();
            Wpf.Ui.Appearance.Theme.Apply(this);
        }

    }
}
