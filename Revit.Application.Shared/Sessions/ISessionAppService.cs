using System.Threading.Tasks;
using Abp.Application.Services;
using Revit.Sessions.Dto;

namespace Revit.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
