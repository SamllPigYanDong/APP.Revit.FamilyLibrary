using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos;
using Revit.Entity.Entity.Dtos.Project;
using Revit.Entity.Entity.Parameters;
using Revit.Service.ApiServices;
using Revit.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Revit.Service.Services
{
    public class ProjectService : BaseService<ProjectDto>, IProjectService
    {
        public ProjectService(MyHttpClient client) : base(client)
        {
            this.ServiceName = "Projects";
        }


        public async Task<ApiResponse<IEnumerable<ProjectDto>>> GetProjects(ProjectQueryParameter queryParameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Get;
            request.Route = $"api/{ServiceName}?pageIndex={queryParameter.PageIndex}" +
                $"&&pageSize={queryParameter.PageSize}" +
                $"&&userId={queryParameter.UserId}" +
                $"&&searchMessage={queryParameter.SearchMessage}";
            return await client.ExecuteAsync<IEnumerable<ProjectDto>>(request);
        }


        public async Task<ApiResponse<ProjectDto>> Create(ProjectCreateDto createDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Post;
            request.Route = $"api/{ServiceName}";
            if (!string.IsNullOrWhiteSpace(createDto.Icon))
            {
                request.FilePaths = new List<string>() { createDto.Icon };
            }
            request.ContentType = ContentType.FormData;
            request.FormDatas = new Dictionary<string, object>
            {
                { "ProjectName", createDto.ProjectName },
                { "ProjectAddress", createDto.ProjectAddress },
                { "Introduction", createDto.Introduction },
                { "CreatorId", createDto.CreatorId }
            };
            return await client.ExecuteAsync<ProjectDto>(request);
        }

        public async Task<ApiResponse> Delete(long projectId)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Delete;
            request.Route = $"api/{ServiceName}/{projectId}";
            return await client.ExecuteAsync(request);
        }


        public async Task<ApiResponse<IEnumerable<ProjectFolderDto>>> GetRecentlyFiles(long userId)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Get;
            request.Route = $"api/{ServiceName}?userId={userId}";
            return await client.ExecuteAsync<IEnumerable<ProjectFolderDto>>(request);
        }


        public async Task<ApiResponse<IEnumerable<UserDto>>> GetUsers(long projectId)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Get;
            request.Route = $"api/{ServiceName}/{projectId}/Users";
            return await client.ExecuteAsync<IEnumerable<UserDto>>(request);
        }

        public async Task<ApiResponse> DeleteUser(long projectId,long userId)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Delete;
            request.Route = $"api/{ServiceName}/{projectId}/Users/{userId}";
            return await client.ExecuteAsync(request);
        }
    }
}
