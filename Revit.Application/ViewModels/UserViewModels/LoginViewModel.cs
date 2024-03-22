using CommunityToolkit.Mvvm.Input;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Revit.Entity;
using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos;
using Revit.Entity.Interfaces;
using Revit.Mvvm;
using Revit.Mvvm.Extensions;
using Revit.Service.IServices;
using Revit.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Revit.Application.ViewModels.UserViewModels
{
    internal class LoginViewModel : ViewModelBase
    {

        private Visibility _progressBarVisibility = Visibility.Hidden;

        public Visibility ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set { SetProperty(ref _progressBarVisibility, value); }
        }

        private LoginDto _loginDto;

        public LoginDto LoginDto
        {
            get { return _loginDto; }
            set { _loginDto = value; }
        }

        private AsyncRelayCommand<Window> _loginCommand;
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;

        public AsyncRelayCommand<Window> LoginCommand { get => _loginCommand ?? new AsyncRelayCommand<Window>(Login); }


        public LoginViewModel(IDataContext dataContext, ILoginService loginService,IUserService userService) : base(dataContext)
        {
            this._loginService = loginService;
            this._userService = userService;
        }

        private async Task Login(Window window)
        {
            var result = await _loginService.Login(new LoginDto() { UserName = "admin", PassWord = "Abc123@" });
            if (result.Code == ResponseCode.Success)
            {
                var getUserResponse = await _userService.GetLoginedUser(result.Content);
                if (getUserResponse.Code == ResponseCode.Success)
                {
                    Global.Token = result.Content;
                    Global.User = getUserResponse.Content;
                    window.DialogResult = true;
                }
            }
        }


        public static void LoginOut()
        {

        }



    }
}
