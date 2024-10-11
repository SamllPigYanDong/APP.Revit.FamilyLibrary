using Revit.Entity.Entity;
using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Commons.Page;
using Revit.Shared.Entity.Users;

namespace Revit.Service.IServices
{
    public interface IUserService
    {
        Task<ApiResponse<PagedList<UserDto>>> GetUsers(UserPageRequestDto pageRequestDto);
    }
}