using AutoMapper;
using Revit.Application.Services;
using Revit.Shared.Models;
using Revit.Shared.Services.App;
using System;
using System.Linq;

namespace Revit.Application.Services;

public class AppMapper : IAppMapper
{
    public AppMapper()
    {
        var configuration = new MapperConfiguration(configure =>
        {
            var assemblys = AppDomain.CurrentDomain.GetAssemblies();
            var targets = assemblys.Where(x => x.FullName.ToLower().Equals("revit.application")).Distinct();
            configure.AddProfile<SharedModuleMapper>();
        });
        Current = configuration.CreateMapper();
    }

    public IMapper Current { get; }

    public TDestination Map<TDestination>(object source) => Current.Map<TDestination>(source);
}
