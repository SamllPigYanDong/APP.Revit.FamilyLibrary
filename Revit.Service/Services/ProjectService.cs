using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos;
using Revit.Entity.Entity.Dtos.Project;
using Revit.Entity.Entity.Parameters;
using Revit.Service.ApiServices;
using Revit.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Revit.Service.Services
{
    public class ProjectService : BaseService<ProjectDto>,IProjectService
    {
        public ProjectService(HttpRestClient client) : base(client)
        {
            this.ServiceName = "Projects";
        }


        public async Task<ApiResponse<IEnumerable<ProjectDto>>>  GetProjects(ProjectQueryParameter queryParameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"api/{ServiceName}?pageIndex={queryParameter.PageIndex}" +
                $"&&pageSize={queryParameter.PageSize}" +
                $"&&userId={queryParameter.UserId}" +
                $"&&searchMessage={queryParameter.SearchMessage}";
            return await  client.ExecuteAsync<IEnumerable<ProjectDto>>(request);
        }


        public async Task<ApiResponse<ProjectDto>> Create(ProjectCreateDto createDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/{ServiceName}";
            request.Parameter=createDto;
            return await client.ExecuteAsync<ProjectDto>(request);
        }

       
    }
}
