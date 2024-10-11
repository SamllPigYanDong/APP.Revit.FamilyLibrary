using Revit.Dto;

namespace Revit.WebHooks.Dto
{
    public class GetAllSendAttemptsInput : PagedInputDto
    {
        public string SubscriptionId { get; set; }
    }
}
