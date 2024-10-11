using Revit.ApiClient;
using Revit.ApiClient.Models;
using Revit.Auditing.Dto;
using Revit.Authorization.Permissions.Dto;
using Revit.Authorization.Roles.Dto;
using Revit.Authorization.Users.Dto;
using Revit.DynamicEntityProperties.Dto;
using Revit.Editions.Dto;
using Revit.Friendships.Dto;
using Revit.Localization.Dto; 
using Revit.MultiTenancy.Dto;
using Revit.Organizations.Dto;
using Revit.Sessions.Dto;
using Revit.Shared.Models;
//using Revit.Shared.Models.Chat;
using Revit.Version.Dtos;
using AutoMapper; 

namespace Revit.Admin.Models
{
    public class AdminModuleMapper : Profile
    {
        public AdminModuleMapper()
        {
            CreateMap<GetAuditLogsFilter, GetAuditLogsInput>().ReverseMap();
            CreateMap<GetEntityChangeFilter, GetEntityChangeInput>().ReverseMap();
            CreateMap<GetTenantsFilter, GetTenantsInput>().ReverseMap();
            CreateMap<FlatPermissionWithLevelDto, PermissionModel>().ReverseMap();

            //系统模块中实体映射关系 
            //CreateMap<FriendDto, FriendModel>().ReverseMap();
            CreateMap<UserListDto, UserListModel>().ReverseMap();
            CreateMap<UserEditDto, UserEditModel>().ReverseMap();
            CreateMap<RoleListDto, RoleListModel>().ReverseMap();
            CreateMap<RoleEditDto, RoleEditModel>().ReverseMap();
            CreateMap<TenantListDto, TenantListModel>().ReverseMap();
            CreateMap<TenantEditDto, TenantListModel>().ReverseMap();
            CreateMap<AuditLogListDto, AuditLogListModel>().ReverseMap();
            CreateMap<UserCreateOrUpdateModel, CreateOrUpdateUserInput>().ReverseMap();
            CreateMap<DynamicPropertyDto, DynamicPropertyModel>().ReverseMap();
            CreateMap<OrganizationUnitDto, OrganizationListModel>().ReverseMap();
            CreateMap<OrganizationUnitDto, OrganizationUnitModel>().ReverseMap();
            CreateMap<LanguageListModel, ApplicationLanguageListDto>().ReverseMap();
            CreateMap<LanguageTextListModel, LanguageTextListDto>().ReverseMap();
            CreateMap<UserLoginInfoDto, UserLoginInfoModel>().ReverseMap();
            CreateMap<UserLoginInfoDto, UserLoginInfoPersistanceModel>().ReverseMap();
            CreateMap<AbpAuthenticateResultModel, AuthenticateResultPersistanceModel>().ReverseMap();
            CreateMap<TenantInformation, TenantInformationPersistanceModel>().ReverseMap();
            CreateMap<TenantLoginInfoDto, TenantLoginInfoPersistanceModel>().ReverseMap();
            CreateMap<ApplicationInfoDto, ApplicationInfoPersistanceModel>().ReverseMap();
            //CreateMap<EditionListDto, EditionListModel>().ReverseMap();
            //CreateMap<EditionCreateDto, EditionCreateModel>().ReverseMap();
            //CreateMap<EditionEditDto, EditionCreateModel>().ReverseMap();
            //CreateMap<FlatFeatureDto, FlatFeatureModel>().ReverseMap();
            CreateMap<FlatPermissionDto, PermissionModel>().ReverseMap();
            CreateMap<GetUserForEditOutput, UserForEditModel>().ReverseMap();
            CreateMap<GetCurrentLoginInformationsOutput, CurrentLoginInformationPersistanceModel>().ReverseMap();
            CreateMap<TenantListModel, CreateTenantInput>().ReverseMap();
            CreateMap<AbpVersionDto, VersionListModel>().ReverseMap();
        }
    }
}
