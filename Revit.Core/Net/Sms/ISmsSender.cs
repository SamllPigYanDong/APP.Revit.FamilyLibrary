using System.Threading.Tasks;

namespace AppFramework.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}