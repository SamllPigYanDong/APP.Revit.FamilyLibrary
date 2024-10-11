using System.Threading.Tasks;
using AppFramework.Authorization.Users;

namespace AppFramework.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
