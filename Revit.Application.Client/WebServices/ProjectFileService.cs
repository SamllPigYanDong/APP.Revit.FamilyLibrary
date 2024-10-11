using Revit.Service.ApiServices;
using Revit.Service.IServices;
using Revit.Shared.Entity.Commons;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Revit.Project.Dto;

namespace Revit.Service.Services
{
    public class ProjectFileService : BaseService<ProjectFolderDto>, IProjectFileService
    {
        public ProjectFileService(MyHttpClient client) : base(client)
        {
        }

        public async Task<ApiResponse<IEnumerable<ProjectFolderDto>>> UploadFilesAsync(long folderId, UploadFileDtoBase projectUploadFileDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Post;
            request.Route = $"api/Folder/{folderId}/File";
            request.FilePaths = projectUploadFileDto.FilesPath;
            return await client.ExecuteAsync<IEnumerable<ProjectFolderDto>>(request);
        }

    }
}
