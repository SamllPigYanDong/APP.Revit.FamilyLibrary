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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Revit.Application.Views.FamilyViews
{
    /// <summary>
    /// FamilyLibaryView.xaml 的交互逻辑
    /// </summary>
    public partial class FamilyLibaryManageView : UserControl
    {
        public FamilyLibaryManageView()
        {
            InitializeComponent();

            this.Resources = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/Revit.Application;component/Styles/Dictionary.xaml")
            };
        }
    }
}
