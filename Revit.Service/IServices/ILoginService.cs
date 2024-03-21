using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Service.IServices
{
    public interface ILoginService
    {
        Task<ApiResponse<string>> Login(LoginDto user);

        Task<ApiResponse> Resgiter(LoginDto user);
    }
}
