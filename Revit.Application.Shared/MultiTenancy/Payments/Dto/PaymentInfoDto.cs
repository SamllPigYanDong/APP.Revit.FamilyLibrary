using Revit.Editions.Dto;

namespace Revit.MultiTenancy.Payments.Dto
{
    public class PaymentInfoDto
    {
        public EditionSelectDto Edition { get; set; }

        public decimal AdditionalPrice { get; set; }

        public bool IsLessThanMinimumUpgradePaymentAmount()
        {
            return AdditionalPrice < AppFrameworkConsts.MinimumUpgradePaymentAmount;
        }
    }
}
