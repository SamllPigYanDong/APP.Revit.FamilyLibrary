using Revit.Accounts.Dto;
using Revit.Shared.Entity.Commons;
using System.Threading.Tasks;

namespace Revit.Service.IServices
{
    public interface IAccountService
    {
        Task<ApiResponse<LoginedUserDto>> GetLoginedUser(string content);
    }
}