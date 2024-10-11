using Revit.Shared;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Ioc;
using Revit.Shared.Services.App;
using Revit.Shared.Services;
using Revit.Entity.Interfaces;

namespace Revit.Admin.ViewModels
{
    public class TaskBarViewModel : BindableBase
    {
        private readonly IHostDialogService dialog;
        private readonly IAppStartService appStartService;
        public DelegateCommand ExitCommand { get; set; }
        public DelegateCommand ShowViewCommand { get; private set; }

        public TaskBarViewModel()
        {
            //dialog = CommandBase.Instance.Container.Resolve<IHostDialogService>();
            //appStartService= CommandBase.Instance.Container.Resolve<IAppStartService>();

            ExitCommand = new DelegateCommand(Exit);
            ShowViewCommand = new DelegateCommand(ShowView);
        }

        private async void Exit()
        {
            ShowView();

            if (await dialog.Question(Local.Localize("AreYouSure")))
                appStartService.Exit();
        }

        private void ShowView()
        { 
            if (!System.Windows.Application.Current.MainWindow.IsVisible)
            {
                System.Windows.Application.Current.MainWindow.Show();
                System.Windows.Application.Current.MainWindow.WindowState = System.Windows.WindowState.Normal;
            }
        }
    }
}
