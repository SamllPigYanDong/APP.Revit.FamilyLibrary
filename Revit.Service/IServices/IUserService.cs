using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos;

namespace Revit.Service.IServices
{
    public interface IUserService
    {
        Task<ApiResponse<LoginedUserDto>> GetLoginedUser(string content);
    }
}