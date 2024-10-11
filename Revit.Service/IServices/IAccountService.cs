using Revit.Entity.Entity.Account;
using Revit.Shared.Entity.Commons;

namespace Revit.Service.IServices
{
    public interface IAccountService
    {
        Task<ApiResponse<LoginedUserDto>> GetLoginedUser(string content);
    }
}