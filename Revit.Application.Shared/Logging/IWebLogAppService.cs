using Abp.Application.Services;
using Revit.Dto;
using Revit.Logging.Dto;

namespace Revit.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
