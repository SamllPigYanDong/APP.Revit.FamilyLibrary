using System.Collections.ObjectModel;
using Revit.Entity.Interfaces;
using Revit.Application.ViewModels;
using Revit.Application.ViewModels.FamilyViewModels;
using Revit.Shared.Entity.Family;
using Revit.Categories;
using Revit.Families;
using Revit.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Revit.Service.Services
{
    public class CategoryService : ViewModelBase, ICategoryService
    {
        private readonly ICategoryAppService categoryWebService;

        public CategoryService( ICategoryAppService categoryWebService) : base()
        {
            this.categoryWebService = categoryWebService;
        }

        public async Task<ViewCategoryDto> GetTreeViewCategories()
        {
            var result = await categoryWebService.GetAllCategories();
            var root = new ViewCategoryDto() { Name = "全部", Id = 0, ParentId = 0 };
            if (result!=null&&result.Any())
            {
                GroupCategories(root, result.ToList().ConvertAll(x=>new ViewCategoryDto(x) ));
            }
            return root;
        }

        private ViewCategoryDto GroupCategories(ViewCategoryDto root, IEnumerable<ViewCategoryDto> categories)
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

        public IEnumerable<long> GetCategoryChildIds(ViewCategoryDto root)
        {
            var results = new List<long>() { root.Id};
            foreach (var child in root.Childs)
            {
                results.Add(child.Id);
                if (child.Childs.Any())
                {
                    results.AddRange(GetCategoryChildIds(child));
                }
            }
            return results;
        }


        public IEnumerable<ViewCategoryDto> GetCategories(ViewCategoryDto root)
        {
            var results = new List<ViewCategoryDto>() { root };
            foreach (var child in root.Childs)
            {
                results.Add(child);
                if (child.Childs.Any())
                {
                    results.AddRange(GetCategories(child));
                }
            }
            return results;
        }

    }


}
