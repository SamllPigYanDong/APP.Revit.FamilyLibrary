using AutoMapper;

namespace Revit.Shared.Interfaces
{
    public interface IAppMapper
    {
        IMapper Current { get; }

        TDestination Map<TDestination>(object source);
    }
}
