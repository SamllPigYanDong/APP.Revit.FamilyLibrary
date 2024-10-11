using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Revit.Authorization.Roles.Dto;
using Revit.Shared.Entity.Commons.Page;
using Revit.Shared.Entity.Permissions;

namespace Revit.Authorization.Roles
{
    /// <summary>
    /// Application service that is used by 'role management' page.
    /// </summary>
    public interface IRoleAppService : IApplicationService
    {
        Task<PagedList<RoleDto>> GetRoles(RolePageRequestDto input);

        //Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdDto input);

        //Task CreateOrUpdateRole(CreateOrUpdateRoleInput input);

        Task DeleteRole(EntityDto input);

        Task<List<PermissionDto>> GetRolePermissions(long id);
    }
}