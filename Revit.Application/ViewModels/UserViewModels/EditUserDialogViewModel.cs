using Autodesk.Revit.DB;
using Prism.Services.Dialogs;
using Revit.Authorization.Users.Dto;
using Revit.Entity.Interfaces;
using Revit.Service.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Revit.Application.ViewModels.UserViewModels
{
    internal class EditUserDialogViewModel : ViewModelBase, IDialogAware
    {
        public EditUserDialogViewModel() 
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

        protected override void Submit()
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
