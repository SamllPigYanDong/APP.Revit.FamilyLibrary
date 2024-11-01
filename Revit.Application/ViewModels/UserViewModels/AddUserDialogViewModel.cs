using Autodesk.Revit.DB;
using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Services.Dialogs;
using Revit.Application.Models;
using Revit.Authorization.Roles;
using Revit.Service.Services;
using Revit.Shared;
using Revit.Shared.Entity.Roles;
using Revit.Shared.Entity.Users;
using Revit.Shared.Extensions.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Revit.Application.ViewModels.UserViewModels
{
    public partial class AddUserDialogViewModel : DialogViewModel
    {
        public AddUserDialogViewModel()
        {
                
        }
        public AddUserDialogViewModel(IRoleAppService roleAppService) 
        {
            this.roleAppService = roleAppService;
        }

        public string Title => "添加用户";

        [ObservableProperty]
        private UserCreateDto _user = new UserCreateDto();
        
        [ObservableProperty]
        private ObservableCollection<RoleDto> _roles=new ObservableCollection<RoleDto>();
        private readonly IRoleAppService roleAppService;

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
           
            return true;
        }

        public void OnDialogClosed()
        {

            
        }

        public override async void OnDialogOpened(IDialogParameters parameters)
        {
            await SetBusyAsync(async () =>
            {
                await roleAppService.GetAllRoles().WebAsync(successCallback: (result) => {
                    Roles = new ObservableCollection<RoleDto>(result);
                    return Task.CompletedTask;
                });
            } );
        }

        protected  void Submit()
        {
            if (string.IsNullOrWhiteSpace(User.UserName) || string.IsNullOrWhiteSpace(User.Password))
            {
                MessageBox.Show("未填写用户名或密码");
                return;
            }
            var buttonResult = ButtonResult.OK;
            var parameters = new DialogParameters();
            parameters.Add(nameof(UserCreateDto), User);
            var result = new Prism.Services.Dialogs.DialogResult(buttonResult, parameters);
            RequestClose.Invoke(result);
        }
        
    }
}
