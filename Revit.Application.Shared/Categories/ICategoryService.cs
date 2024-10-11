using Revit.Families;
using Revit.Shared.Entity.Family;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revit.Service.Services
{
    public interface ICategoryService
    {
        IEnumerable<long> GetCategoryChildIds(ViewCategoryDto root);
        Task<ViewCategoryDto> GetTreeViewCategories();
        IEnumerable<ViewCategoryDto> GetCategories(ViewCategoryDto root);
    }
}