using AutoMapper;
using Revit.Shared.Entity.Roles;

namespace Revit.Shared.Models
{
    public class SharedModuleMapper : Profile
    {
        public SharedModuleMapper()
        {
            CreateMap<RoleCreateDto, RoleCreateDto>().ReverseMap();
        }
    }
}