using CommunityToolkit.Mvvm.ComponentModel;
using Revit.Shared.Entity.Commons;

namespace Revit.Entity.Entity
{
    public class ViewEntityBase : ViewDtoBase
    {

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { SetProperty(ref _isChecked, value); }
        }


    



        public ViewEntityBase()
        {
        }

    }
}
