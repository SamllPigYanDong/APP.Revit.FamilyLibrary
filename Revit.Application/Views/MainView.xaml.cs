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
using System.Windows.Shapes;
using Wpf.Ui.Appearance;
using Prism.Ioc;
using Revit.Application.ViewModels;
using Prism.Regions;

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
            //this.regionManager = regionManager;
            //ApplicationThemeManager.Apply(this);
            //ApplicationThemeManager.Changed += ApplicationThemeManager_Changed; 
            //this.Unloaded += (s, e) =>
            //{
            //    ApplicationThemeManager.Changed -= ApplicationThemeManager_Changed;
            //};
        }

        //private void ApplicationThemeManager_Changed(ApplicationTheme currentApplicationTheme, Color systemAccent)
        //{
        //    ApplicationThemeManager.Apply(this);
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var theme = ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Dark ? ApplicationTheme.Light : ApplicationTheme.Dark;
        //    ApplicationThemeManager.Apply(theme);
        //}

        //private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (this.regionManager.Regions["MainContent"]!=null)
        //    {
        //        MessageBox.Show(this.regionManager.Regions["MainContent"].Views.Count().ToString());
        //        this.regionManager.RequestNavigate("MainContent", "ProjectView");
        //    }
        //    else
        //    {
        //        MessageBox.Show("null");
        //    }
           
        //}
    }
}
