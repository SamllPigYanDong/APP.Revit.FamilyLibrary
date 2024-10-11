using Revit.Shared.Entity.Family;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revit.Categories
{
    public interface ICategoryAppService
    {
        Task<CategoryDto> AddCategory(CategoryCreateDto categoryCreateDto);
        Task<int> DeleteCategory(long categoryId);
        Task<IEnumerable<CategoryDto>> GetAllCategories();
    }
}