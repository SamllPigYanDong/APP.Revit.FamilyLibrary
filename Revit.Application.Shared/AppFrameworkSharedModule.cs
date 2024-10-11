using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Revit
{
    [DependsOn(typeof(AppFrameworkCoreSharedModule))]
    public class AppFrameworkSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AppFrameworkSharedModule).GetAssembly());
        }
    }
}