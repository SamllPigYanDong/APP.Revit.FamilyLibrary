using Prism.Mvvm;
using Revit.Service.Contants;

namespace Tamplate.Revit.Mep.ViewModels
{
    public class ProgressBarViewModel : BindableBase
    {       
        private int _value;
        private string _title;
        private int _max;
        public int Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public int Max
        {
            get { return _max; }
            set { SetProperty(ref _max, value); }
        }
        public ProgressBarViewModel()
        {
            MessengerInstance.Register<int>(this, ServiceTokens.ProgressBarDialog_Max, (max) =>
            {
                this.Max = max;   
            });
            MessengerInstance.Register<string>(this, ServiceTokens.ProgressBarDialog_Title, (title) =>
            {                
                Value++;
                this.Title = $"{Value}/{Max}_{title}";
                System.Windows.Forms.Application.DoEvents();
            });
        }

    }
}
