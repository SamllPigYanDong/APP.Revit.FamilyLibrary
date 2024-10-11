using Revit.Commons;
using Revit.Shared.Entity.Family;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Families
{
    public class ViewCategoryDto : ViewEntityBase
    {
        public long ParentId { get; set; }

        public string Name { get; set; } = "";

        public CategoryType CategoryType { get; set; }

        public ObservableCollection<ViewCategoryDto> _childs = new ObservableCollection<ViewCategoryDto>();

        public ObservableCollection<ViewCategoryDto> Childs
        {
            get { return _childs; }
            set { SetProperty(ref _childs, value); }
        }

        public ViewCategoryDto()
        {

        }


        public ViewCategoryDto(CategoryDto x)
        {
            ParentId = x.ParentId;
            CategoryType = x.CategoryType;
            Name = x.Name;
            Id = x.Id;
        }


    }
}
