using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows;
using Autodesk.Revit.Attributes;
using Prism.Ioc;
using Revit.Application.ViewModels;
using Revit.Application.Views;
using Revit.Extension;
using Revit.Mvvm.Extensions;
using Revit.Application.Views.ProjectViews;
using Revit.Application.ViewModels.ProjectViewModels;
using Revit.Application.Views.FamilyViews;
using Revit.Application.ViewModels.FamilyViewModels;
using Tuna.Revit.Extension;
using System.Linq;
using Revit.Shared.Base;
using Revit.Shared;

namespace Revit.Application.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    public class TestImageCommand : CommandBase
    {

        public override Window CreateMainWindow()
        {
            return Instance.Container.Resolve<MainView,MainViewModel>();
        }

        public override Result Execute(string message, ElementSet elements)
        {

            return Result.Succeeded;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainProjectView,MainProjectViewModel>();
            containerRegistry.RegisterForNavigation<TotalProjectView, DisPlayProjectViewModel>();
            containerRegistry.RegisterForNavigation<RecentlyProjectView, DisPlayProjectViewModel>();
            containerRegistry.RegisterForNavigation<ProjectFileManageView, ProjectFileManageViewModel>();
            containerRegistry.RegisterForNavigation<ProjectMemberView, ProjectMemberViewModel>();
            containerRegistry.RegisterForNavigation<ProjectView, ProjectViewModel>();
        }
    }
}
