using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos.Project;
using Revit.Entity.Project;

namespace Revit.Service.Services
{
    public interface IProjectFileService
    {
        Task<ApiResponse<IEnumerable<ProjectFolderDto>>> UploadFilesAsync(long folderId, ProjectUploadFileDto projectUploadFileDto);
    }
}