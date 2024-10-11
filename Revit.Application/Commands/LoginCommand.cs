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
using System;
using Revit.Application.Views.FamilyViews.Public;
using Revit.Application.Views.FamilyViews.Public.DialogViews;
using Revit.Application.ViewModels.FamilyViewModels.PublicViewModels;
using Revit.Application.ViewModels.FamilyViewModels;
using Revit.Application.Views.FamilyViews;
using Revit.Application.ViewModels.FamilyViewModels.DialogViewModels;
using Revit.Application.ViewModels.FamilyViewModels.PublicViewModels.DialogViewModels;
using Revit.Application.ViewModels.UserViewModels;
using Revit.Application.Views.UserViews;
using Revit.Application.Views.UserViews.DialogViews;
using Autodesk.Revit.UI.Selection;



//翻页存在问题需要解决 √
//程序报错时会直接崩溃需要排查一下 √
//补充新Usercontrol模块 进行成员管理以及权限管理

//需对族进行分类，分类模块如何书写，此项单独一个表√
//另族需要新增tag标签，分类其实也是标签的一种√
//标签需要限制同一父类下增加相同标签，标签需要可选标签类型

//模型审核可选标签，需要进行分类，按标签类型进行分类 √
//屏蔽全部标签可选
//模型审核需要可批量进行

//公共族库页码有问题


//审核模块补充备注入数据库
//补充上传列表获取
//日志管理模块
namespace Revit.Application.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    public class LoginCommand :CommandBase
    {

        public override System.Windows.Window CreateMainWindow()
        {
            if (!LoginExtension.IsUserLogin())
            {
                return null;
            }
            
            return Container.Resolve<MainView,MainViewModel>();
        }

        public override Result Execute(string message, ElementSet elements)
        {
            TransactionStatus transactionStatus = TransactionStatus.Started;
            try
            {
#if !NETCOREAPP
                //ResolveHelper.BeginAssemblyResolve(GetType()); objecttype
                
#endif
                transactionStatus = DocumentExtension.NewTransactionGroup(DataContext.Document, "族库管理", () => MainWindow.ShowDialog().Value);
            }
            catch (Exception exception)
            {
                return Result.Cancelled;
            }
            finally
            {
#if !NETCOREAPP
                //ResolveHelper.EndAssemblyResolve();
#endif
            }
            return transactionStatus == TransactionStatus.Committed ? Result.Succeeded: Result.Cancelled;
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
            containerRegistry.RegisterForNavigation<FamilyLibaryPublicView, FamilyLibraryPublicViewModel>();
            containerRegistry.RegisterForNavigation<FamilyLibaryPublicUploadView, FamilyLibaryPublicUploadViewModel>();
            containerRegistry.RegisterForNavigation<FamilyLibraryPublicAuditView, FamilyLibraryPublicAuditViewModel>();
            containerRegistry.RegisterForNavigation<FamilyLibrayManagerView, FamilyLibraryManagerViewModel>();
            containerRegistry.RegisterForNavigation<WorkSpaceView, WorkSpaceViewModel>();
            containerRegistry.RegisterForNavigation<UeserManageView, UserManageViewModel>();


            containerRegistry.RegisterDialog<EditUserDialogView, EditUserDialogViewModel>();
            containerRegistry.RegisterDialog<AuditingFamilyDialogView, AuditingFamilyDialogViewModel>();
            containerRegistry.RegisterDialog<CreateCatagoryDialogView, CreateCatagoryDialogViewModel>();
        }
    }
}
