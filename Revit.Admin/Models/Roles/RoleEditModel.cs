using CommunityToolkit.Mvvm.ComponentModel; 

namespace Revit.Admin.Models
{
    [INotifyPropertyChanged]
    public partial class RoleEditModel 
    {
        [ObservableProperty]
        private string displayName;

        [ObservableProperty]
        private bool isDefault;

        public long? Id { get; set; } 
    }
}