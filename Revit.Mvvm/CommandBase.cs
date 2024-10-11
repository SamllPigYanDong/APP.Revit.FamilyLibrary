using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows;
using Revit.Service.Services;
using Prism.Ioc;
using Prism.Regions;
using Prism.DryIoc;
using DryIoc;
using Revit.Entity;
using Revit.Entity.Interfaces;
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
using Revit.Application.ViewModels.FamilyViewModels;
using CommonServiceLocator;
using Revit.Authorization.Roles;
using Revit.Application;
using Abp.Dependency;
using Revit.ApiClient;
using Revit.Authorization.Permissions;
using Revit.Families;
using Revit.Authorization.Users;
using Revit.Categories;
using Revit.Shared.Models;
using System.Windows.Forms;

namespace Revit.Mvvm
{
    public abstract class CommandBase : IExternalCommand
    {
        protected IDataContext DataContext { get => _containerExtension.Resolve<DataContext>(); }//无窗体时拿到Doc和UIDoc

        public static Window MainWindow { get; set; }

        public abstract Window CreateMainWindow();
        public abstract Result Execute(string message, ElementSet elements);

        public static CommandBase Instance { get; private set; }

        private readonly IContainerExtension _containerExtension = CreateContainerExtension();

        private readonly IModuleCatalog _moduleCatalog = new ModuleCatalog();

        public IContainerProvider Container => _containerExtension;

        public CommandBase()
        {
            Instance = this;
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)//程序主入口
        {
            try
            {
                ViewModelLocationProvider.SetDefaultViewModelFactory((view, type) => Container.Resolve(type));
                _containerExtension.RegisterInstance(commandData);
                _containerExtension.RegisterInstance(_containerExtension);
                _containerExtension.RegisterInstance(_moduleCatalog);

                RegisterRequiredTypes(_containerExtension);
                RegisterTypes(_containerExtension);

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
                System.Windows.MessageBox.Show(e.Message + e.InnerException?.Message);
                return Result.Cancelled;
            }

            return Result.Succeeded;
        }


       

        /// <summary>
        /// Used to register types with the container that will be used by your application.
        /// </summary>
        protected abstract void RegisterTypes(IContainerRegistry containerRegistry);

        /// <summary>
        /// Configures the <see cref="IModuleCatalog"/> used by Prism.
        /// </summary>
        protected virtual void ConfigureModuleCatalog(IModuleCatalog moduleCatalog) { }

        private static IContainerExtension CreateContainerExtension()
        {
            Rules rules = Rules.Default.WithAutoConcreteTypeResolution()
                .WithDefaultIfAlreadyRegistered(IfAlreadyRegistered.Replace)
                .With(Made.Of(FactoryMethod.ConstructorWithResolvableArguments));
            //Rules rules = Rules.Default;
            return new DryIocContainerExtension(new Container(rules));
        }

        private static void RegisterRequiredTypes(IContainerRegistry registry)
        {
            // Base WPF Application types
            registry.RegisterSingleton<ILoggerFacade, TextLogger>();
            registry.RegisterSingleton<IDialogService,Revit.Mvvm.Prism.DialogService>();
            registry.RegisterSingleton<IModuleInitializer, ModuleInitializer>();
            registry.RegisterSingleton<IModuleManager, ModuleManager>();
            registry.RegisterSingleton<RegionAdapterMappings>();
            registry.RegisterSingleton<IRegionManager, RegionManager>();
            registry.RegisterSingleton<IEventAggregator, EventAggregator>();
            registry.RegisterSingleton<IRegionViewRegistry, RegionViewRegistry>();
            registry.RegisterSingleton<IRegionBehaviorFactory, RegionBehaviorFactory>();
            registry.Register<IRegionNavigationJournalEntry, RegionNavigationJournalEntry>();
            registry.Register<IRegionNavigationJournal, RegionNavigationJournal>();
            registry.Register<IRegionNavigationService, RegionNavigationService>();
            registry.Register<IDialogWindow, DialogWindow>(); //default dialog host

            // Special WPF Application types
            registry.RegisterSingleton<IRegionNavigationContentLoader, RegionNavigationContentLoader>();
            registry.RegisterSingleton<IServiceLocator, DryIocServiceLocatorAdapter>();

            //注册Doucment
            registry.RegisterSingleton<IDataContext, DataContext>();
            registry.RegisterInstance(new MyHttpClient(Global.HOST));
            registry.RegisterInstance(new AbpApiClient());

            registry.Register<ILoginService, LoginService>();
            registry.Register<IAccountService, AccountService>();

            registry.Register<ICategoryService, CategoryService>();
            registry.Register<ICategoryAppService, CategoryAppService>();
            registry.Register<IFamilyService, FamilyService>();
            registry.Register<IFamilyAppService, FamilyAppService>();

            registry.Register<IProjectService, ProjectService>();
            registry.Register<IProjectFolderService, ProjectFolderService>();
            registry.Register<IProjectFileService, ProjectFileService>();

            registry.Register<IRoleAppService, RoleAppService>();
            registry.Register<IPermissionAppService, PermissionAppService>();
            registry.Register<IUserAppService, ProxyUsersAppService>();
           

        }

    }
}
