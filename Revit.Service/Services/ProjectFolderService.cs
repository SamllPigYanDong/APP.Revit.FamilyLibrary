using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos.Project;
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
    public class ProjectFolderService : BaseService<ProjectFolderDto>, IProjectFolderService
    {
        public ProjectFolderService(MyHttpClient client) : base(client)
        {
            ServiceName = "Folder";
        }

        public async Task<ApiResponse<IEnumerable<ProjectFolderDto>>> GetFolders(long projectId, ProjectRequestFolderDto projectRequestFolderDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Get;
            request.Route = $"api/Project/{projectId}/{ServiceName}" +
                $"?requestPath={projectRequestFolderDto.RequestPath}";
            return await client.ExecuteAsync<IEnumerable<ProjectFolderDto>>(request);
        }


    }



}
