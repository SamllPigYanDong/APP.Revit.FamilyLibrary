using System.Threading.Tasks;
using Abp.Application.Services;
using Revit.MultiTenancy.Payments.Dto;
using Revit.MultiTenancy.Payments.Stripe.Dto;

namespace Revit.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}