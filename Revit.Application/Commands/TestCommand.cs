using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows;
using Autodesk.Revit.Attributes;
using Prism.Ioc;
using Revit.Application.ViewModels;
using Revit.Application.Views;
using Revit.Extension;
using Revit.Mvvm.Extensions;
using Revit.Shared.Base;

namespace Revit.Application.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    [Regeneration(RegenerationOption.Manual)]
    public class TestCommand : CommandBase
    {

        public override Window CreateMainWindow()
        {
            return/* Container.Resolve<TestView, TestViewModel>()*/null;
        }

        public override Result Execute(string message, ElementSet elements)
        {
            TransactionStatus transactionStatus = DocumentExtension.NewTransactionGroup(DataContext.Document, "族库管理", () => MainWindow.ShowDialog().Value);
            return transactionStatus == TransactionStatus.Committed ? Result.Succeeded : Result.Cancelled;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
