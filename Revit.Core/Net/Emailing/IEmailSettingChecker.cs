using System.Threading.Tasks;

namespace AppFramework.Net.Emailing
{
    public interface IEmailSettingsChecker
    {
        bool EmailSettingsValid();

        Task<bool> EmailSettingsValidAsync();
    }
}