using System.Collections.Generic;

namespace AppFramework.MultiTenancy.Payments
{
    public interface IPaymentGatewayStore
    {
        List<PaymentGatewayModel> GetActiveGateways();
    }
}
