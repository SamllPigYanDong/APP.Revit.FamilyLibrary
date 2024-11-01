using AutoMapper;
using Revit.Application.Models;
using Revit.Application.Models.Permission;
using Revit.Shared.Entity.Permissions;
using Revit.Shared.Entity.Roles;

namespace AppFramework.Admin.Models;

public class AdminModuleMapper : Profile
{
    public AdminModuleMapper()
    {
        
        CreateMap<FlatPermissionWithLevelDto, PermissionModel>().ReverseMap();

        //系统模块中实体映射关系 
      
        CreateMap<FlatPermissionDto, PermissionModel>().ReverseMap();
      
    }
}
