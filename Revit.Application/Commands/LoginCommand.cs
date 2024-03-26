using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows;
using Autodesk.Revit.Attributes;
using Prism.Ioc;
using Revit.Application.ViewModels;
using Revit.Application.Views;
using Revit.Extension;
using Revit.Mvvm.Extensions;
using Revit.Mvvm;
using Revit.Application.Views.ProjectViews;
using Revit.Application.ViewModels.ProjectViewModels;
using Revit.Application.ViewModels.ProjectViewModels.ProjectDialogViewModels;

namespace Revit.Application.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    public class LoginCommand :CommandBase
    {

        public override Window CreateMainWindow()
        {
            return Container.Resolve<MainView,MainViewModel>();
        }

        public override Result Execute(string message, ElementSet elements)
        {
            if (!LoginExtension.IsUserLogin())
            {
                return Result.Cancelled;
            }
            TransactionStatus transactionStatus = DocumentExtension.NewTransactionGroup(DataContext.Document, "族库管理", () => MainWindow.ShowDialog().Value);
            return transactionStatus == TransactionStatus.Committed ? Result.Succeeded : Result.Cancelled;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterLoginTypes();

            containerRegistry.RegisterForNavigation<MainProjectView,MainProjectViewModel>();
            containerRegistry.RegisterForNavigation<TotalProjectView, DisPlayProjectViewModel>();
            containerRegistry.RegisterForNavigation<RecentlyProjectView, DisPlayProjectViewModel>();
            containerRegistry.RegisterForNavigation<ProjectFileManageView, ProjectFileManageViewModel>();
            containerRegistry.RegisterForNavigation<ProjectMemberView, ProjectMemberViewModel>();
            containerRegistry.RegisterForNavigation<ProjectView, ProjectViewModel>();
            containerRegistry.RegisterForNavigation<WorkSpaceView, WorkSpaceViewModel>();
        }
    }
}
