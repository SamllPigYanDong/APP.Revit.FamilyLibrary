using System.Collections.ObjectModel;
using Revit.Application.ViewModels;
using Revit.Application.ViewModels.FamilyViewModels;
using Revit.Shared.Entity.Family;
using Revit.Categories;
using Revit.Families;
using Revit.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Abp.Application.Services.Dto;
using Revit.Shared;

namespace Revit.Service.Services
{
    public static class CategoryExtensions
    {
        public static  ViewCategoryDto GetTreeViewCategories(this ListResultDto<CategoryDto> cateDto)
        {
            var root = new ViewCategoryDto() { Name = "全部", Id = 0, ParentId = 0 };
            root = root.GroupCategories(cateDto.Items.Where(x=>x.CategoryType==CategoryType.Major).ToList().ConvertAll(x => new ViewCategoryDto(x)));
            return root;
        }

        private static ViewCategoryDto GroupCategories(this ViewCategoryDto root, IEnumerable<ViewCategoryDto> categories)
        {
            root.Childs.AddRange(categories.Where(x => x.ParentId == root.Id));
            if (root.Childs != null)
            {
                foreach (var child in root.Childs)
                {
                    GroupCategories(child, categories);
                }
            }
            return root;
        }

        public static IEnumerable<long> GetNodeCategoriesIds(this ViewCategoryDto root)
        { 
           return GetNodeCategories(root).Select((x) => x.Id);
        }

        public static IEnumerable<ViewCategoryDto> GetNodeCategories(this ViewCategoryDto root)
        {
            var results = new List<ViewCategoryDto>() { root };
            foreach (var child in root.Childs)
            {
                results.Add(child);
                if (child.Childs.Any())
                {
                    results.AddRange(GetNodeCategories(child));
                }
            }
            return results;
        }

    }


}
