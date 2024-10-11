using Revit.Authorization.Users.Dto;
using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Commons.Page;
using System.Threading.Tasks;

namespace Revit.Service.IServices
{
    public interface IUserService
    {
        Task<ApiResponse<PagedList<UserDto>>> GetUsers(UserPageRequestDto pageRequestDto);
    }
}