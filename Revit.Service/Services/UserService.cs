using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos;
using Revit.Entity.Entity.Parameters;
using Revit.Service.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Service.Services
{
    public class UserService : BaseService<LoginedUserDto>, IUserService
    {
        public UserService(MyHttpClient client) : base(client)
        {
            this.ServiceName = "Accounts";
        }

        public async Task<ApiResponse<LoginedUserDto>> GetLoginedUser(string token)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Get;
            request.Route = $"api/{ServiceName}";
            request.Token= token;
            request.ContentType= ContentType.Json;
            return await client.ExecuteAsync<LoginedUserDto>(request);
        }


    }
}
