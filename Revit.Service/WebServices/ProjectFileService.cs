using Revit.Entity.Entity;
using Revit.Service.ApiServices;
using Revit.Service.IServices;
using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Project;
using System.Net.Http;

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
