using Autodesk.Revit.UI;
using Prism.Ioc;
using Prism.DryIoc;
using DryIoc;
using System;
using Revit.Entity;
using Revit.Shared.Interfaces;
using Revit.Mvvm.Interface;
using Revit.Mvvm;

namespace Revit.Shared.Base
{
    public abstract class ApplicationBase : IExternalApplication
    {

        private IContainerExtension _containerExtension;

        public abstract void RegisterTypes(IContainerExtension container);//其他插件自定义服务注入到IOC中

        public Result OnShutdown(UIControlledApplication application)
        {
            var events = _containerExtension.Resolve<IEventManager>();
            if (events != null)
            {
                events.Unsubscribe();
            }
            return Result.Succeeded;
        } //关闭程序

        public Result OnStartup(UIControlledApplication application)
        {
            RegisterMethod(application);
            //事件订阅
            var events = _containerExtension.Resolve<IEventManager>();
            if (events != null)
            {
                events.Subscribe();
            }
            //创建按钮 UI
            var appUI = _containerExtension.Resolve<IApplication>();
            return appUI == null ? Result.Cancelled : appUI.Initial();
        }//启动程序



        private void RegisterMethod(UIControlledApplication application)
        {
            _containerExtension.Register<IUIProvider, UIProvider>();
            RegisterTypes(_containerExtension);
        }
    }
}
