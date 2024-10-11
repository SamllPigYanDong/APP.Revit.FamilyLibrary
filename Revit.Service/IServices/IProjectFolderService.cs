using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Project;

namespace Revit.Service.IServices
{
    public interface IProjectFolderService
    {
        Task<ApiResponse<ProjectFolderDto>> CreateFolder(long projectId, ProjectCreateFolderDto projectCreateFolderDto);
        Task<ApiResponse<IEnumerable<ProjectFolderDto>>> GetFolders(long projectId, ProjectGetFoldersDto projectRequestFolderDto);
    }
}