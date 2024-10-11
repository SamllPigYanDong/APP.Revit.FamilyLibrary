using Revit.Shared.Entity.Commons;
using System.Threading.Tasks;
using System.Collections.Generic;
using Revit.Project.Dto;

namespace Revit.Service.IServices
{
    public interface IProjectFileService
    {
        Task<ApiResponse<IEnumerable<ProjectFolderDto>>> UploadFilesAsync(long folderId, UploadFileDtoBase projectUploadFileDto);
    }
}