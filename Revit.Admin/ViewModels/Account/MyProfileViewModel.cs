using Revit.ApiClient;
using Revit.Shared;
using Revit.Admin.Models;
using Revit.Admin.ViewModels.Shared;
using Prism.Services.Dialogs; 

namespace Revit.Admin.ViewModels
{
    public class MyProfileViewModel : HostDialogViewModel
    {
        private readonly IApplicationContext applicationContext;

        public MyProfileViewModel(IApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        private UserLoginInfoModel userInfo;

        public UserLoginInfoModel UserInfo
        {
            get { return userInfo; }
            set { userInfo = value; OnPropertyChanged(); }
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            UserInfo = Map<UserLoginInfoModel>(applicationContext.LoginInfo.User);
        }
    }
}
