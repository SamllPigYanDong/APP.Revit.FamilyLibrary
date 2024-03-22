using Revit.Application.UI;
using Revit.Application.Views;
using Revit.Entity.Entity;
using Revit.Entity;
using Revit.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Revit.Service.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using System.Windows;
using Prism.Ioc;
using Prism.Regions;
using Prism.Commands;
using Revit.Entity.Entity.Dtos;

namespace Revit.Application.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;

        private ObservableCollection<MenuBar> _menuBars = new ObservableCollection<MenuBar>() {
        new MenuBar() { Icon = "Home", Title = "工作台", NameSpace = nameof(WorkSpaceView) },
        new MenuBar() { Icon = "Project", Title = "项目", NameSpace = nameof(ProjectView) },
        };
        public ObservableCollection<MenuBar> MenuBars
        {
            get { return _menuBars; }
            set { SetProperty(ref _menuBars, value); }
        }


        private readonly IUserService _userService;
        private DelegateCommand<MenuBar> _navigateCommand;
        public DelegateCommand<MenuBar> NavigateCommand
        {
            get => _navigateCommand ?? new DelegateCommand<MenuBar>(Navigate);
        }

      

        public MainViewModel(IDataContext dataContext, IRegionManager regionManager, IUserService userService) : base(dataContext)
        {
            this._regionManager = regionManager;
            this._userService = userService;
            this._regionManager.RequestNavigate("MainContent", MenuBars.FirstOrDefault().NameSpace);
        }

      



        private void Navigate(MenuBar navigation)
        {
            if (navigation != null && !string.IsNullOrWhiteSpace(navigation.NameSpace))
            {
                MessageBox.Show(this._regionManager.Regions["MainContent"].Views.Count().ToString());
                this._regionManager.RequestNavigate("MainContent", navigation.NameSpace);
            }
        }






    }
}
