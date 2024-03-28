using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos.Project;

namespace Revit.Service.IServices
{
    public interface IProjectFolderService
    {
        Task<ApiResponse<IEnumerable<ProjectFolderDto>>> GetFolders(long projectId, ProjectRequestFolderDto projectRequestFolderDto);
    }
}