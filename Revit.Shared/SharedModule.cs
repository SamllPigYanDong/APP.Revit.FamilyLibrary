using CommonServiceLocator;
using DryIoc;
using Prism.DryIoc.Ioc;
using Prism.DryIoc;
using Prism.Events;
using Prism.Ioc;
using Prism.Logging;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions.Behaviors;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using Container = DryIoc.Container;

namespace Revit.Shared
{
    public class SharedModule:IModule
    {
        public static Window MainWindow { get; set; }

        public static SharedModule Instance { get; private set; }

        private readonly IModuleCatalog _moduleCatalog = new ModuleCatalog();

        private readonly IContainerExtension _containerExtension = CreateContainerExtension();

        public IContainerProvider Container => _containerExtension;

        public SharedModule()
        {
            Instance = this;
        }

        public void PrismInit(Action<IContainerExtension> action)
        {
            //设置默认的Viewmodel的解析规则
            ViewModelLocationProvider.SetDefaultViewModelFactory((view, type) => Container.Resolve(type));
            _containerExtension.RegisterInstance(_containerExtension);
            _containerExtension.RegisterInstance(_moduleCatalog);
            action.Invoke(_containerExtension);

            RegisterRequiredTypes(_containerExtension);

            _containerExtension.FinalizeExtension();

            ServiceLocator.SetLocatorProvider(() => Container.Resolve<IServiceLocator>());


            ConfigureModuleCatalog(_moduleCatalog);

            RegionAdapterMappings regionAdapterMappings = Container.Resolve<RegionAdapterMappings>();
            if (regionAdapterMappings != null)
            {
                regionAdapterMappings.RegisterMapping(typeof(Selector), Container.Resolve<SelectorRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ItemsControl), Container.Resolve<ItemsControlRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ContentControl), Container.Resolve<ContentControlRegionAdapter>());
            }
            //区域行为工厂
            IRegionBehaviorFactory regionBehaviorFactory = Container.Resolve<IRegionBehaviorFactory>();
            if (regionBehaviorFactory != null)
            {
                regionBehaviorFactory.AddIfMissing(BindRegionContextToDependencyObjectBehavior.BehaviorKey, typeof(BindRegionContextToDependencyObjectBehavior));
                regionBehaviorFactory.AddIfMissing(RegionActiveAwareBehavior.BehaviorKey, typeof(RegionActiveAwareBehavior));
                regionBehaviorFactory.AddIfMissing(SyncRegionContextWithHostBehavior.BehaviorKey, typeof(SyncRegionContextWithHostBehavior));
                regionBehaviorFactory.AddIfMissing(RegionManagerRegistrationBehavior.BehaviorKey, typeof(RegionManagerRegistrationBehavior));
                regionBehaviorFactory.AddIfMissing(RegionMemberLifetimeBehavior.BehaviorKey, typeof(RegionMemberLifetimeBehavior));
                regionBehaviorFactory.AddIfMissing(ClearChildViewsRegionBehavior.BehaviorKey, typeof(ClearChildViewsRegionBehavior));
                regionBehaviorFactory.AddIfMissing(AutoPopulateRegionBehavior.BehaviorKey, typeof(AutoPopulateRegionBehavior));
                regionBehaviorFactory.AddIfMissing(IDestructibleRegionBehavior.BehaviorKey, typeof(IDestructibleRegionBehavior));
            }

            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ActivationException));
            ExceptionExtensions.RegisterFrameworkExceptionType(typeof(ContainerException));

            Container.Resolve<IModuleManager>()?.Run();
        }

        /// <summary>
        ///  配置模Prism块化类别 <see cref="IModuleCatalog"/> .
        /// </summary>
        protected virtual void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {


        }

        private static IContainerExtension CreateContainerExtension()
        {
            Rules rules = Rules.Default.WithAutoConcreteTypeResolution()
                .WithDefaultIfAlreadyRegistered(IfAlreadyRegistered.Replace)
                .With(Made.Of(FactoryMethod.ConstructorWithResolvableArguments));
            return new DryIocContainerExtension(new Container(rules));
        }

        private static void RegisterRequiredTypes(IContainerRegistry container)
        {
            // 基础WPF应用类的注册
            container.RegisterSingleton<ILoggerFacade, TextLogger>();
            container.RegisterSingleton<IDialogService, Revit.Shared.Prism.DialogService>();
            container.RegisterSingleton<IModuleInitializer, ModuleInitializer>();
            container.RegisterSingleton<IModuleManager, ModuleManager>();
            container.RegisterSingleton<RegionAdapterMappings>();
            container.RegisterSingleton<IRegionManager, RegionManager>();
            container.RegisterSingleton<IEventAggregator, EventAggregator>();
            container.RegisterSingleton<IRegionViewRegistry, RegionViewRegistry>();
            container.RegisterSingleton<IRegionBehaviorFactory, RegionBehaviorFactory>();
            container.Register<IRegionNavigationJournalEntry, RegionNavigationJournalEntry>();
            container.Register<IRegionNavigationJournal, RegionNavigationJournal>();
            container.Register<IRegionNavigationService, RegionNavigationService>();
            container.Register<IDialogWindow, DialogWindow>(); //default dialog host

            // WPF应用类的注册
            container.RegisterSingleton<IRegionNavigationContentLoader, RegionNavigationContentLoader>();
            container.RegisterSingleton<IServiceLocator, DryIocServiceLocatorAdapter>();

            SharedModuleExtensions.AddSharedServices(container);
        }

        public void RegisterTypes(IContainerRegistry container)
        {
            // 基础WPF应用类的注册
            container.RegisterSingleton<ILoggerFacade, TextLogger>();
            container.RegisterSingleton<IDialogService, Revit.Shared.Prism.DialogService>();
            container.RegisterSingleton<IModuleInitializer, ModuleInitializer>();
            container.RegisterSingleton<IModuleManager, ModuleManager>();
            container.RegisterSingleton<RegionAdapterMappings>();
            container.RegisterSingleton<IRegionManager, RegionManager>();
            container.RegisterSingleton<IEventAggregator, EventAggregator>();
            container.RegisterSingleton<IRegionViewRegistry, RegionViewRegistry>();
            container.RegisterSingleton<IRegionBehaviorFactory, RegionBehaviorFactory>();
            container.Register<IRegionNavigationJournalEntry, RegionNavigationJournalEntry>();
            container.Register<IRegionNavigationJournal, RegionNavigationJournal>();
            container.Register<IRegionNavigationService, RegionNavigationService>();
            container.Register<IDialogWindow, DialogWindow>(); //default dialog host

            // WPF应用类的注册
            container.RegisterSingleton<IRegionNavigationContentLoader, RegionNavigationContentLoader>();
            container.RegisterSingleton<IServiceLocator, DryIocServiceLocatorAdapter>();

            SharedModuleExtensions.AddSharedServices(container);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
           
        }
    }
}
