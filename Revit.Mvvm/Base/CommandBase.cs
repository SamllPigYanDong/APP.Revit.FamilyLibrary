using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows;
using Revit.Service.Services;
using Prism.Ioc;
using Prism.Regions;
using Prism.DryIoc;
using DryIoc;
using Revit.Entity;
using Revit.Service.ApiServices;
using Revit.Service.IServices;
using System;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions.Behaviors;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using Prism.Events;
using Prism.Services.Dialogs;
using Prism.Logging;
using Prism.DryIoc.Ioc;
using CommonServiceLocator;
using Revit.Authorization.Roles;
using Revit.Application;
using Revit.ApiClient;
using Revit.Authorization.Permissions;
using Revit.Families;
using Revit.Authorization.Users;
using Revit.Categories;
using Revit.Mvvm.Interface;
using System.ComponentModel;

namespace Revit.Shared.Base
{
    public abstract class CommandBase : IExternalCommand
    {
        protected IDataContext DataContext { get => SharedModule.Instance.Container.Resolve<DataContext>(); }//无窗体时拿到Doc和UIDoc

        public static Window MainWindow { get; set; }

        public abstract Window CreateMainWindow();

        public abstract Result Execute(string message, ElementSet elements);


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)//CMD程序主入口
        {
            try
            {
                SharedModule.Instance.PrismInit((contanier => {
                    contanier.RegisterInstance(commandData);
                    contanier.RegisterSingleton<IDataContext, DataContext>();
                    RegisterTypes(contanier);
                }));

                var window = CreateMainWindow();
                if (window != null)//如果为null则该功能无主窗体
                {
                    MainWindow = window;
                }
                //执行命令
                Execute(message, elements);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.InnerException?.Message);
                return Result.Cancelled;
            }

            return Result.Succeeded;
        }

        /// <summary>
        /// 用来注册需要使用的类别
        /// </summary>
        protected abstract void RegisterTypes(IContainerRegistry containerRegistry);
    }
}
