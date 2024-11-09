using Revit.Project.Dto;
using Revit.Shared.Entity.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Web.Models;

namespace Revit.Service.IServices
{
    public interface IProjectFolderService
    {
        Task<AjaxResponse<ProjectFolderDto>> CreateFolder(long projectId, ProjectCreateFolderDto projectCreateFolderDto);
        Task<AjaxResponse<IEnumerable<ProjectFolderDto>>> GetFolders(long projectId, ProjectGetFoldersDto projectRequestFolderDto);
    }
}