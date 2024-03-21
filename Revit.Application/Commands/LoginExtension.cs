using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Prism.Ioc;
using Prism.Mvvm;
using Revit.Application.ViewModels.UserViewModels;
using Revit.Application.Views.UserViews;
using Revit.Entity;
using Revit.Mvvm;
using Revit.Mvvm.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Revit.Application.Commands
{
    public static class LoginExtension 
    {
        public static  void RegisterLoginTypes(this IContainerRegistry containerRegistry)
        {
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
