using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Family;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Web.Models;

namespace Revit.Application.ViewModels.FamilyViewModels
{
    public interface ICategoryWebService
    {
        Task<AjaxResponse<CategoryDto>> AddCategory(CategoryCreateDto categoryCreateDto);
        Task<AjaxResponse<int>> DeleteCategory(long categoryId);
        Task<AjaxResponse<IEnumerable<CategoryDto>>> GetCategories();
    }
}