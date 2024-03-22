using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos.Project;
using Revit.Entity.Entity.Parameters;
using Revit.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Service.IServices
{
    public interface IProjectService
    {
        Task<ApiResponse<IEnumerable<ProjectDto>>> GetProjects(ProjectQueryParameter pagedList);
        Task<ApiResponse<ProjectDto>> Create(ProjectCreateDto createDto);
        Task<ApiResponse> Delete(long projectId);
    }
}
