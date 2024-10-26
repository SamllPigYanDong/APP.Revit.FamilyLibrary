using Autodesk.Revit.DB;
using Prism.Services.Dialogs;
using Revit.Authorization.Users.Dto;
using Revit.Service.Services;
using Revit.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Revit.Application.ViewModels.UserViewModels
{
    internal class AddUserDialogViewModel : ViewModelBase, IDialogAware
    {
        public AddUserDialogViewModel() 
        {
        }

        public string Title => "添加用户";

        private UserCreateDto _user = new UserCreateDto();
        public UserCreateDto User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }


        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
           
            return true;
        }

        public void OnDialogClosed()
        {

            
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


        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
