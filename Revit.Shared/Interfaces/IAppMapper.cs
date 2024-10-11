using AutoMapper;

namespace Revit.Entity.Interfaces
{
    public interface IAppMapper
    {
        IMapper Current { get; }

        TDestination Map<TDestination>(object source);
    }
}
