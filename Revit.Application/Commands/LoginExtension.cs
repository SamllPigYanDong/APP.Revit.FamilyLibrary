using Prism.Ioc;
using Revit.Application.ViewModels.ProjectViewModels.ProjectDialogViewModels;
using Revit.Application.ViewModels.UserViewModels;
using Revit.Application.Views.ProjectViews.ProjectDialogs;
using Revit.Application.Views.UserViews;
using Revit.Entity;
using Revit.Mvvm;

namespace Revit.Application.Commands
{
    public static class LoginExtension 
    {
        public static  void RegisterLoginTypes(this IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterDialogWindow<DefaultDialog>();
            containerRegistry.RegisterDialog<ProjectCreateDialog, ProjectCreateDialogViewModel>();

            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();

        }

        public static bool IsUserLogin()
        {
            if (Global.User != null)
            {
                return true;
            }
            else
            {
                var loginView =CommandBase.Instance.Container.Resolve<LoginView>();
                if (loginView!=null)
                {
                    return loginView.ShowDialog().Value;
                }
            }
            return false;
        }

    }
}
