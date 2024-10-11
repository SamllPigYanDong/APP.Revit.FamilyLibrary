using Abp;

namespace AppFramework
{
    /// <summary>
    /// This class can be used as a base class for services in this application.
    /// It has some useful objects property-injected and has some basic methods most of services may need to.
    /// It's suitable for non domain nor application service classes.
    /// For domain services inherit <see cref="AppFrameworkDomainServiceBase"/>.
    /// For application services inherit AppFrameworkAppServiceBase.
    /// </summary>
    public abstract class AppFrameworkServiceBase : AbpServiceBase
    {
        protected AppFrameworkServiceBase()
        {
            LocalizationSourceName = AppFrameworkConsts.LocalizationSourceName;
        }
    }
}