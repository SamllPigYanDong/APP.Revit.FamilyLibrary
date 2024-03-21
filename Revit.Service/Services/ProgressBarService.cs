using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Revit.Service.Contants;
using Tamplate.Revit.Mep.IServices;
using Tamplate.Revit.Mep.ViewModels;
using Tamplate.Revit.Mep.Views;

namespace Tamplate.Revit.Mep.Services
{
    public class ProgressBarService : IProgressBarService//进度条处理（实体层）
    {
        private ProgressBarDialog _progressBarDialog;
        public void Start(int max)
        {
            _progressBarDialog = new ProgressBarDialog();
            _progressBarDialog.DataContext = new ProgressBarViewModel();
            _progressBarDialog.Show();
            Messenger.Default.Send<int>(max,ServiceTokens.ProgressBarDialog_Max);
        }

        public void Stop()
        {
            Messenger.Default.Unregister(_progressBarDialog);
            Messenger.Default.Unregister(_progressBarDialog.DataContext);
            _progressBarDialog.Close();
        }
    }
}
