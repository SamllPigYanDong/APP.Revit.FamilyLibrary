using AutoMapper; 

namespace Revit.Shared.Services.Mapper
{
    public interface IAppMapper
    {
        IMapper Current { get; }

        TDestination Map<TDestination>(object source);
    }
}
