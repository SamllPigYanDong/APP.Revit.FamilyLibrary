using Revit.Application.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Regions;
using Prism.Commands;
using Revit.Application.Views.ProjectViews;
using Revit.Application.Views.FamilyViews;
using Revit.Service.IServices;
using Revit.Entity;
using Revit.Accounts.Dto;
using Revit.Shared;
using Revit.ApiClient.Models;

namespace Revit.Application.ViewModels
{
    public partial class MainViewModel : ViewModelBase
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


        public LoginedUserDto User
        {
            get { return Global.User; }
        }


        private DelegateCommand<MenuBar> _navigateCommand;

        [ObservableProperty]
        private  AbpAuthenticateModel _abpAuthenticateModel;

        public DelegateCommand<MenuBar> NavigateCommand
        {
            get => _navigateCommand ?? new DelegateCommand<MenuBar>(Navigate);
        }

      

        public MainViewModel( IRegionManager regionManager, AbpAuthenticateModel abpAuthenticateModel)
        {
            this._regionManager = regionManager;
            _abpAuthenticateModel = abpAuthenticateModel;
        }

      



        private void Navigate(MenuBar navigation)
        {
            if (navigation != null && !string.IsNullOrWhiteSpace(navigation.NameSpace))
            {
                this._regionManager.RequestNavigate("MainContent", navigation.NameSpace);
            }
        }






    }
}
