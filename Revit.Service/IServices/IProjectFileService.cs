using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos.Project;

namespace Revit.Service.IServices
{
    public interface IProjectFileService
    {
        Task<ApiResponse<IEnumerable<ProjectFolderDto>>> UploadFilesAsync(long folderId, UploadFileDtoBase projectUploadFileDto);
    }
}