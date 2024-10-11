using Abp.Application.Services.Dto;
using Revit.ApiClient;
using Revit.Authorization.Roles;
using Revit.Authorization.Roles.Dto;
using Revit.Shared.Entity.Commons.Page;
using Revit.Shared.Entity.Permissions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revit.Application
{
    public class RoleAppService : ProxyAppServiceBase, IRoleAppService
    {
        public RoleAppService(AbpApiClient apiClient) : base(apiClient)
        {
        }

        //public async Task CreateOrUpdateRole(CreateOrUpdateRoleInput input)
        //{
        //    await ApiClient.PostAsync(GetEndpoint(nameof(CreateOrUpdateRole)), input);
        //}

        public async Task DeleteRole(EntityDto input)
        {
            await ApiClient.DeleteAsync(GetEndpoint(nameof(DeleteRole)), input);
        }

        //public async Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdDto input)
        //{
        //    return await ApiClient.GetAsync<GetRoleForEditOutput>(GetEndpoint(nameof(GetRoleForEdit)), input);
        //}

        public async Task<List<PermissionDto>> GetRolePermissions(long id)
        {
            return await ApiClient.GetAsync<List<PermissionDto>>($"api/Roles/{id}/permissions");
        }


        public async Task<PagedList<RoleDto>> GetRoles(RolePageRequestDto input)
        {
            return await ApiClient.GetAsync<PagedList<RoleDto>>("api/Roles", input);
        } 
    }
}