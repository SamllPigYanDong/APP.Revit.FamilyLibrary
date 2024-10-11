using Revit.Application.Views;
using Revit.Entity.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Prism.Regions;
using Prism.Commands;
using Revit.Application.Views.ProjectViews;
using Revit.Application.Views.FamilyViews;
using Revit.Service.IServices;
using Revit.Entity.Entity.UI;
using Revit.Entity;
using Revit.Accounts.Dto;

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


        public LoginedUserDto User
        {
            get { return Global.User; }
        }


        private readonly IAccountService _userService;
        private DelegateCommand<MenuBar> _navigateCommand;
        public DelegateCommand<MenuBar> NavigateCommand
        {
            get => _navigateCommand ?? new DelegateCommand<MenuBar>(Navigate);
        }

      

        public MainViewModel( IRegionManager regionManager, IAccountService userService) 
        {
            this._regionManager = regionManager;
            this._userService = userService;
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
