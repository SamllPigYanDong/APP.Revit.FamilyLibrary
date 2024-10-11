using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Revit.Common.Dto;
using Revit.Editions.Dto;

namespace Revit.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();
    }
}