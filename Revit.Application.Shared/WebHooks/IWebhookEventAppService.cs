using System.Threading.Tasks;
using Abp.Webhooks;

namespace Revit.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
