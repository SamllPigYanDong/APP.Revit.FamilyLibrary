using System.Threading.Tasks;
using Abp.Application.Services;
using Revit.Configuration.Host.Dto;

namespace Revit.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
