using CommunityToolkit.Mvvm.Input;
using Revit.Accounts.Dto;
using Revit.Entity;
using Revit.Service.IServices;
using Revit.Shared;
using Revit.Shared.Entity.Commons;
using System.Threading.Tasks;
using System.Windows;

namespace Revit.Application.ViewModels.UserViewModels
{
    public partial class LoginViewModel : ViewModelBase
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
        private readonly ILoginService _loginService;
        private readonly IAccountService _userService;



        public LoginViewModel( ILoginService loginService, IAccountService userService)
        {
            this._loginService = loginService;
            this._userService = userService;
        }
        [RelayCommand]
        private async Task Login(Window window)
        {
            var result = await _loginService.Login(new LoginDto() { UserName = "admin", PassWord = "Abc123@" });
            ProgressBarVisibility = Visibility.Visible;
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
            ProgressBarVisibility = Visibility.Hidden;
        }


        public static void LoginOut()
        {

        }



    }
}
