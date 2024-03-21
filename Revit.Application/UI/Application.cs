using DryIoc;
using Prism.Ioc;
using Revit.Entity;
using Revit.Entity.Interfaces;
using Revit.Mvvm;
using Revit.Service.Services;

namespace Revit.Application.UI
{
    public class Application : ApplicationBase
    {
        public override void RegisterTypes(IContainerExtension container)
        {
            //注册UI、事件
            container.RegisterSingleton<IDataContext, DataContext>();
            container.Register<IEventManager, ApplicationEvent>();
            container.Register<IApplication, ApplicationUI>();
        }
    }
}
