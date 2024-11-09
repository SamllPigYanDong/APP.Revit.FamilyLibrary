using Abp.Application.Services.Dto;
using Revit.Families;
using Revit.Shared.Entity.Family;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revit.Categories
{
    public interface ICategoryAppService
    {
        Task<CategoryDto> AddCategory(CategoryCreateDto categoryCreateDto);
        Task<int> DeleteCategory(long categoryId);
        Task<ListResultDto<CategoryDto>> GetListAsync();
        Task<ListResultDto<CategoryDto>> GetCategories(ViewCategoryDto viewCategoryDto);
        int GetCategoryChildIds(ViewCategoryDto category);
        Task<ViewCategoryDto> GetTreeViewCategories();
    }
}