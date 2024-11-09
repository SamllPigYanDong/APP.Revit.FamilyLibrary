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
using Revit.Shared.Services.App;
using Revit.Authorization.Accounts;
using Revit.IServices;

namespace Revit.Shared
{
    public static class SharedModuleExtensions
    {
        public static void AddSharedServices(this IContainerRegistry registry)
        {
            registry.RegisterSingleton<IGlobalValidator, GlobalValidator>();
            registry.RegisterSingleton<AbpApiClient>();
            registry.RegisterSingleton<AbpAuthenticateModel>();
            registry.RegisterSingleton<IAccessTokenManager, AccessTokenManager>();

            registry.RegisterInstance(new MyHttpClient(Global.HOST));
            registry.Register<IDataPagerService, DataPagerService>();
            registry.Register<IPermissionAppService, PermissionAppService>();
            registry.Register<IAuthsAppService, AuthsAppService>();
            registry.Register<IAccountService, AccountService>();
            registry.Register<IProjectService, ProjectService>();
            registry.Register<IProjectFolderService, ProjectFolderService>();
            registry.Register<IProjectFileService, ProjectFileService>();

            //旧service


            registry.Register<IAuditLogAppService, AuditLogAppService>();
            registry.Register<IPermissionAppService, PermissionAppService>();
            registry.Register<IUserAppService, ProxyUsersAppService>();
            registry.Register<IRoleAppService, RolesAppService>();
            registry.Register<ICategoryAppService, CategoryAppService>();
            registry.Register<IFamilyAppService, FamilyAppService>();
        }
    }
}
