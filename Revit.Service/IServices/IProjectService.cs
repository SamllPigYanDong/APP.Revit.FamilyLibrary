using Revit.Shared.Entity.Commons;
using Revit.Entity.Entity.Parameters;
using Revit.Entity.Entity.Project;
using Revit.Shared.Entity.Project;
using Revit.Shared.Entity.Users;

namespace Revit.Service.IServices
{
    public interface IProjectService
    {
        Task<ApiResponse<IEnumerable<ProjectDto>>> GetProjects(ProjectPageRequestDto pagedList);
        Task<ApiResponse<ProjectDto>> Create(ProjectPostPutDto createDto);
        Task<ApiResponse> Delete(long projectId);

        Task<ApiResponse<IEnumerable<ProjectFolderDto>>> GetRecentlyFiles(long userId);

        Task<ApiResponse<IEnumerable<UserDto>>> GetUsers(long projectId);
        Task<ApiResponse> DeleteUser(long projectId, long userId);
    }
}
