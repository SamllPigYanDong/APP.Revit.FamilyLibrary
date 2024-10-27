using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows;
using Prism.Ioc;
using DryIoc;
using Revit.Entity;
using System;
using Revit.Mvvm.Interface;
using Prism.Modularity;

namespace Revit.Shared.Base
{
    public abstract class CommandBase : SharedModule, IExternalCommand
    {
        protected IDataContext DataContext { get => Container.Resolve<DataContext>(); }//无窗体时拿到Doc和UIDoc

        public CommandBase():base()
        {
        }

        public abstract Window CreateMainWindow();

        public abstract Result Execute(string message, ElementSet elements);


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)//CMD程序主入口
        {
            try
            {
                PrismInit((contanier =>
                {
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
