using Prism.Ioc;
using Revit.Shared.Interfaces;
using Revit.Application.Services;
using Revit.Shared.Services.App;
using Revit.Categories;
using Revit.Mvvm.Services;
using Revit.Service.IServices;

namespace Revit.Application.Services
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IContainerRegistry services)
        {
            services.RegisterSingleton<IAppMapper, AppMapper>();
            services.RegisterSingleton<IFamilyService, FamilyService>();
        }
    }
}
