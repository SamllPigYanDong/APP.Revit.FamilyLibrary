using Revit.Accounts.Dto;
using Revit.Shared.Entity.Commons;
using System.Threading.Tasks;

namespace Revit.Service.IServices
{
    public interface ILoginService
    {
        Task<ApiResponse<string>> Login(LoginDto user);

        Task<ApiResponse> Resgiter(LoginDto user);
    }
}
