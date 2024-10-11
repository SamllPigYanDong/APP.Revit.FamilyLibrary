using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Revit.Authorization.Users.Dto;

namespace Revit.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<PagedResultDto<UserLoginAttemptDto>> GetUserLoginAttempts(GetLoginAttemptsInput input);
    }
}
