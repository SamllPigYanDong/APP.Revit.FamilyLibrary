using AppFramework.Admin.Models;
using AutoMapper;
using Revit.Application.Models.Permission;
using Revit.Application.Models.Users;
using Revit.Families;
using Revit.Shared.Entity.Family;
using Revit.Shared.Entity.Permissions;
using Revit.Shared.Entity.Users;

namespace Revit.Application.Models
{
    public class AdminModuleMapper : Profile
    {
        public AdminModuleMapper()
        {
        
            CreateMap<FlatPermissionWithLevelDto, PermissionModel>().ReverseMap();

            //系统模块中实体映射关系 
            CreateMap<CategoryDto,ViewCategoryDto>().ReverseMap();
            CreateMap<FlatPermissionDto, PermissionModel>().ReverseMap();
            CreateMap<UserEditDto, UserEditModel>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(x => x.EmailAddress, opt => opt.MapFrom(x => x.EmailAddress))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(x => x.IsActive)).ReverseMap();
            CreateMap<GetUserForEditOutput, UserForEditModel>().ForMember(x => x.User, opt => opt.MapFrom(x => x.User))
                .ReverseMap();


        }
    }
}
