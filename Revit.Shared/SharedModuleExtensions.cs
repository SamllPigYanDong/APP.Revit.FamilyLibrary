using Abp.Configuration.Startup;
using Revit.ApiClient.Models;
using Revit.ApiClient;
using Revit.Application.Client;
using Revit.Application;
using Revit.Auditing;
using Revit.Authorization.Permissions;
using Revit.Authorization.Roles;
using Revit.Authorization.Users;
using Prism.Ioc;
using Revit.Shared.Validations;
using Revit.Shared.Services.Datapager;
using Revit.Service.ApiServices;
using Revit.Entity;
using Revit.Service.IServices;
using Revit.Service.Services;
using Revit.Categories;
using Revit.Families;
using Revit.Shared.Interfaces;

namespace Revit.Shared
{
    public static class SharedModuleExtensions
    {
        public static void AddSharedServices(this IContainerRegistry registry)
        {
            registry.RegisterInstance(new MyHttpClient(Global.HOST));
            registry.RegisterInstance(new AbpApiClient());
            registry.Register<IDataPagerService, DataPagerService>();
            registry.RegisterSingleton<AbpApiClient>();
            registry.RegisterSingleton<AbpAuthenticateModel>();
            registry.Register<IRoleAppService, RoleAppService>();
            registry.Register<IAuditLogAppService, AuditLogAppService>();
            registry.Register<IPermissionAppService, PermissionAppService>();
            registry.Register<ILoginService, LoginService>();
            registry.Register<IAccountService, AccountService>();
            registry.Register<ICategoryAppService, CategoryAppService>();
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
