using Abp.Application.Services.Dto;
using Revit.ApiClient;
using Revit.Authorization.Users.Dto;
using Revit.Dto;
using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Commons.Page;
using System.Net.Http;
using System.Threading.Tasks;

namespace Revit.Authorization.Users
{
    public class ProxyUsersAppService : ProxyAppServiceBase, IUserAppService
    {

        public new const string ApiBaseUrl = "api/users";

        public ProxyUsersAppService(AbpApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<PagedList<UserDto>> GetUsers(UserPageRequestDto pageRequestDto)
        {
            return await ApiClient.GetAsync<PagedList<UserDto>>(GetEndpoint(), pageRequestDto);
        }

        //public async Task<FileDto> GetUsersToExcel(GetUsersToExcelInput input)
        //{
        //    return await ApiClient.GetAsync<FileDto>(GetEndpoint(nameof(GetUsersToExcel)), input);
        //}

        //public async Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input)
        //{
        //    return await ApiClient.GetAsync<GetUserForEditOutput>(GetEndpoint(nameof(GetUserForEdit)), input);
        //}

        //public async Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(EntityDto<long> input)
        //{
        //    return await ApiClient.GetAsync<GetUserPermissionsForEditOutput>(GetEndpoint(nameof(GetUserPermissionsForEdit)), input);
        //}

        public async Task ResetUserSpecificPermissions(EntityDto<long> input)
        {
            await ApiClient.PostAsync(GetEndpoint(nameof(ResetUserSpecificPermissions)), input);
        }

        //public async Task UpdateUserPermissions(UpdateUserPermissionsInput input)
        //{
        //    await ApiClient.PutAsync(GetEndpoint(nameof(UpdateUserPermissions)), input);
        //}

        public async Task<UserDto> CreateOrUpdateUser(UserCreateDto input)
        {
           return await ApiClient.PostAsync<UserDto>(ApiBaseUrl, input);
        }

        public async Task DeleteUser(EntityDto<long> input)
        {
            await ApiClient.DeleteAsync(ApiBaseUrl+$"/id",input);
        }

        public async Task UnlockUser(EntityDto<long> input)
        {
            await ApiClient.PostAsync(GetEndpoint(nameof(UnlockUser)), input);
        }
    }
}