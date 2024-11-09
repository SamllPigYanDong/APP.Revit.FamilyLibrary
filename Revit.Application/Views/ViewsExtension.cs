using Prism.Ioc;
using Revit.Application.ViewModels.FamilyViewModels.DialogViewModels;
using Revit.Application.ViewModels.FamilyViewModels.PublicViewModels.DialogViewModels;
using Revit.Application.ViewModels.FamilyViewModels.PublicViewModels;
using Revit.Application.ViewModels.FamilyViewModels;
using Revit.Application.ViewModels.ProjectViewModels;
using Revit.Application.ViewModels.UserViewModels;
using Revit.Application.ViewModels;
using Revit.Application.Views.FamilyViews.Public.DialogViews;
using Revit.Application.Views.FamilyViews.PublicViews;
using Revit.Application.Views.FamilyViews;
using Revit.Application.Views.ProjectViews;
using Revit.Application.Views.UserViews.DialogViews;
using Revit.Application.Views.UserViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Revit.Application.ViewModels.ProjectViewModels.ProjectDialogViewModels;
using Revit.Application.Views.ProjectViews.ProjectDialogs;

namespace Revit.Application.Views
{
    public static class ViewsExtension
    {
        public static void AddViews(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Add<MainProjectView, MainProjectViewModel>();
            containerRegistry.Add<TotalProjectView, DisPlayProjectViewModel>();
            containerRegistry.Add<RecentlyProjectView, DisPlayProjectViewModel>();
            containerRegistry.Add<ProjectFileManageView, ProjectFileManageViewModel>();
            containerRegistry.Add<ProjectMemberView, ProjectMemberViewModel>();
            containerRegistry.Add<ProjectView, ProjectViewModel>();
            containerRegistry.Add<FamilyLibraryPublicView, FamilyLibraryPublicViewModel>();
            containerRegistry.Add<FamilyLibaryPublicUploadView, FamilyLibraryPublicUploadViewModel>();
            containerRegistry.Add<FamilyLibraryPublicAuditView, FamilyLibraryPublicAuditViewModel>();
            containerRegistry.Add<FamilyLibrayManagerView, FamilyLibraryManagerViewModel>();
            containerRegistry.Add<WorkSpaceView, WorkSpaceViewModel>();
            containerRegistry.Add<AccountManagerView, AccountManagerViewViewModel>();
            containerRegistry.Add<RoleView, RoleViewModel>();
            containerRegistry.Add<UserView, UserViewModel>();
            containerRegistry.Add<LoginView, LoginViewModel>(); 

            containerRegistry.RegisterDialog<ProjectCreateDialog, ProjectCreateDialogViewModel>();
            containerRegistry.RegisterDialog<AddUserDialogView, AddUserDialogViewModel>();
            containerRegistry.RegisterDialog<AddRoleDialogView, AddRoleDialogViewModel>();
            containerRegistry.RegisterDialog<AuditingFamilyDialogView, AuditingFamilyDialogViewModel>();
            containerRegistry.RegisterDialog<CreateCatagoryDialogView, CreateCatagoryDialogViewModel>();
        }

        static void Add<TView, TViewModel>(this IContainerRegistry containerRegistry, string name = null)
        {
            containerRegistry.RegisterForNavigation<TView, TViewModel>(name);
        }
    }
}
