using CommunityToolkit.Mvvm.ComponentModel; 

namespace Revit.Admin.Models
{
    [INotifyPropertyChanged]
    public partial class PagedFilter 
    {
        public PagedFilter()
        {
            MaxResultCount = AppConsts.DefaultPageSize;
        }

        public int MaxResultCount { get; set; }

        public int SkipCount { get; set; }
    }
}
