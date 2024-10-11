using Revit.Shared.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Revit.Admin.Models
{
    public partial class DynamicPropertyModel : EntityObject
    {
        [ObservableProperty]
        private string propertyName;

        [ObservableProperty]
        private string displayName;

        [ObservableProperty]
        private string inputType;

        [ObservableProperty]
        private string permission;
         
        public int? TenantId { get; set; }
    }
}