using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Revit.Authorization.Users.Dto;
using Revit.Dto;
using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Commons.Page;

namespace Revit.Authorization.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task<PagedList<UserDto>> GetUsers(UserPageRequestDto userPageRequestDto);

        //Task<FileDto> GetUsersToExcel(GetUsersToExcelInput input);

        //Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input);

        //Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(EntityDto<long> input);

        //Task ResetUserSpecificPermissions(EntityDto<long> input);

        //Task UpdateUserPermissions(UpdateUserPermissionsInput input);

        Task<UserDto> CreateOrUpdateUser(UserCreateDto input);

        Task DeleteUser(EntityDto<long> input);

        Task UnlockUser(EntityDto<long> input);
    }
}