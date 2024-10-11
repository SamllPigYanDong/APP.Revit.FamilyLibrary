using Revit.Entity.Entity.Family;
using Revit.Shared.Entity.Family;

namespace Revit.Service.Services
{
    public interface ICategoryService
    {
        IEnumerable<long> GetCategoryChildIds(ViewCategoryDto root);
        Task<ViewCategoryDto> GetTreeViewCategories();
        IEnumerable<ViewCategoryDto> GetCategories(ViewCategoryDto root);
    }
}