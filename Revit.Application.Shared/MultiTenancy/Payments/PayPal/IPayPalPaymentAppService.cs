using System.Threading.Tasks;
using Abp.Application.Services;
using Revit.MultiTenancy.Payments.PayPal.Dto;

namespace Revit.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
