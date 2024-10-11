using System.Threading.Tasks;
using Abp.Application.Services;

namespace Revit.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
