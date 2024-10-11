using Abp.Domain.Services;

namespace AppFramework
{
    public abstract class AppFrameworkDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected AppFrameworkDomainServiceBase()
        {
            LocalizationSourceName = AppFrameworkConsts.LocalizationSourceName;
        }
    }
}
