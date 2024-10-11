using System.Threading.Tasks;
using Abp.Application.Services;
using Revit.Editions.Dto;
using Revit.MultiTenancy.Dto;

namespace Revit.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}