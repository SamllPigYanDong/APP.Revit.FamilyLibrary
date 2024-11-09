using CommunityToolkit.Mvvm.Input;
using Revit.Accounts.Dto;
using Revit.Entity;
using Revit.Service.IServices;
using Revit.Shared;
using Revit.Shared.Entity.Commons;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Revit.ApiClient.Models;
using Revit.IServices;

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
        private readonly IAuthsAppService _authsService;

        [ObservableProperty]
        private  AbpAuthenticateModel _abpAuthenticateModel;



        public LoginViewModel( IAuthsAppService authsService, AbpAuthenticateModel abpAuthenticateModel)
        {
            this._authsService = authsService;
            _abpAuthenticateModel = abpAuthenticateModel;
        }

        [RelayCommand]
        private async Task Login(Window window)
        {
            AbpAuthenticateModel.UserNameOrEmailAddress = "admin";
            AbpAuthenticateModel.Password = "Abc123@";
            await _authsService.LoginAsync();
            ProgressBarVisibility = Visibility.Visible;
            if (true)
            {
                    window.DialogResult = true;
            }
            ProgressBarVisibility = Visibility.Hidden;
        }

        public static void LoginOut()
        {

        }
    }
}
