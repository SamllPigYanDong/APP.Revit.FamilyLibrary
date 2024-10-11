using Revit.Entity.Entity;
using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Project;

namespace Revit.Service.IServices
{
    public interface IProjectFileService
    {
        Task<ApiResponse<IEnumerable<ProjectFolderDto>>> UploadFilesAsync(long folderId, UploadFileDtoBase projectUploadFileDto);
    }
}