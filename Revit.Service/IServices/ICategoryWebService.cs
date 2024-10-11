using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Family;

namespace Revit.Application.ViewModels.FamilyViewModels
{
    public interface ICategoryWebService
    {
        Task<ApiResponse<CategoryDto>> AddCategory(CategoryCreateDto categoryCreateDto);
        Task<ApiResponse<int>> DeleteCategory(long categoryId);
        Task<ApiResponse<IEnumerable<CategoryDto>>> GetCategories();
    }
}