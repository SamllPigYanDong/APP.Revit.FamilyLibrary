using Revit.Entity.Entity.Account;
using Revit.Shared.Entity.Commons;

namespace Revit.Service.IServices
{
    public interface ILoginService
    {
        Task<ApiResponse<string>> Login(LoginDto user);

        Task<ApiResponse> Resgiter(LoginDto user);
    }
}
