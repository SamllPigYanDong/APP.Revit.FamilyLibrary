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
#pragma warning disable CS0649 // 从未对字段“MainViewModel._navigateCommand”赋值，字段将一直保持其默认值 null
        private DelegateCommand<MenuBar> _navigateCommand;
#pragma warning restore CS0649 // 从未对字段“MainViewModel._navigateCommand”赋值，字段将一直保持其默认值 null
        public DelegateCommand<MenuBar> NavigateCommand
        {
            get => _navigateCommand ?? new DelegateCommand<MenuBar>(Navigate);
        }

        public MainViewModel(IDataContext dataContext, IRegionManager regionManager, IUserService userService) : base(dataContext)
        {
            this._regionManager = regionManager;
            this._userService = userService;
            //Init();
            this._regionManager.RequestNavigate("MainContent", MenuBars.FirstOrDefault().NameSpace);
        }

        private async void Init()
        {
            var getUserResponse = await _userService.GetLoginedUser();
            if (getUserResponse.Code == ResponseCode.Success)
            {
                Global.User = getUserResponse.Content;
            }
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
