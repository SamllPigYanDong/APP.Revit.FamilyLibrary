using Abp.Application.Services.Dto;
using Abp.PlugIns;
using Revit.ApiClient;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Family;
using System.Linq;
using Revit.Families;

namespace Revit.Categories
{
    public class CategoryAppService : ProxyAppServiceBase, ICategoryAppService
    {
        public CategoryAppService(AbpApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<ListResultDto<CategoryDto>> GetListAsync()
        {
            return await ApiClient.GetAsync<ListResultDto<CategoryDto>>(GetEndpoint());
        }


        public async Task<CategoryDto> AddCategory(CategoryCreateDto categoryCreateDto)
        {
            return await ApiClient.PostAsync<CategoryDto>(GetEndpoint(), categoryCreateDto);
        }


        public async Task<int> DeleteCategory(long categoryId)
        {
            return await ApiClient.DeleteAsync<int>(GetEndpoint($"{categoryId}"), new EntityDto<long>(categoryId));
        }

        public async Task<ListResultDto<CategoryDto>> GetCategories(ViewCategoryDto viewCategoryDto)
        {
            return await ApiClient.GetAsync<ListResultDto<CategoryDto>>(GetEndpoint());
        }

        public int GetCategoryChildIds(ViewCategoryDto category)
        {
            throw new NotImplementedException();
        }

        public Task<ViewCategoryDto> GetTreeViewCategories()
        {
            throw new NotImplementedException();
        }
    }
}
