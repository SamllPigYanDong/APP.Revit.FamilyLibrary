using Revit.Shared.Entity.Commons;
using System.Threading.Tasks;
using System.Collections.Generic;
using Revit.Project.Dto;
using Revit.Authorization.Users.Dto;

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
