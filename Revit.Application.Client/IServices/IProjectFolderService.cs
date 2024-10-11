using Revit.Project.Dto;
using Revit.Shared.Entity.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revit.Service.IServices
{
    public interface IProjectFolderService
    {
        Task<ApiResponse<ProjectFolderDto>> CreateFolder(long projectId, ProjectCreateFolderDto projectCreateFolderDto);
        Task<ApiResponse<IEnumerable<ProjectFolderDto>>> GetFolders(long projectId, ProjectGetFoldersDto projectRequestFolderDto);
    }
}