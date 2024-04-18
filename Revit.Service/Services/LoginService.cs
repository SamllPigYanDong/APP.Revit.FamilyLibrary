﻿using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos;
using Revit.Service.ApiServices;
using Revit.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly MyHttpClient client;
        private readonly string serviceName = "Auths";

        public LoginService(MyHttpClient client)
        {
            this.client = client;
        }

        public async Task<ApiResponse<string>> Login(LoginDto user)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Post;
            request.Route = $"api/{serviceName}/login";
            request.Parameter = user;
            return await client.ExecuteAsync<string>(request);
        }

        public async Task<ApiResponse> Resgiter(LoginDto user)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Post;
            request.Route = $"api/{serviceName}/Resgiter";
            request.Parameter = user;
            return await client.ExecuteAsync(request);
        }
    }
}
