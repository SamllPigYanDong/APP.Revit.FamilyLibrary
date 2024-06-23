using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos.Project;

namespace Revit.Service.IServices
{
    public interface IProjectFolderService
    {
        Task<ApiResponse<ProjectFolderDto>> CreateFolder(long projectId, ProjectCreateFolderDto projectCreateFolderDto);
        Task<ApiResponse<IEnumerable<ProjectFolderDto>>> GetFolders(long projectId, ProjectRequestFolderDto projectRequestFolderDto);
    }
}