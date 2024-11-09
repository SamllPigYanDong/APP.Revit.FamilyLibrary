using Revit.Commons;
using Revit.Service.ApiServices;
using Revit.Service.IServices;
using Revit.Shared.Entity.Commons;
using System.Net.Http;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Web.Models;

namespace Revit.Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected readonly MyHttpClient client;
        protected string ServiceName { get; set; }

        public BaseService(MyHttpClient client)
        {
            this.client = client;
        }

        public async Task<AjaxResponse<TEntity>> AddAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Post;
            request.Route = $"api/{ServiceName}";
            request.Parameter = entity;
            return await client.ExecuteAsync<TEntity>(request);
        }

        public async Task<AjaxResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Delete;
            request.Route = $"api/{ServiceName}/{id}";
            return await client.ExecuteAsync(request);
        }


        public async Task<AjaxResponse<TEntity>> GetFirstOfDefaultAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Get;
            request.Route = $"api/{ServiceName}?id={id}";
            return await client.ExecuteAsync<TEntity>(request);
        }

        public async Task<AjaxResponse<IPagedResult<TEntity>>> GetListAsync(QueryParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Get;
            request.Route = $"api/{ServiceName}?pageIndex={parameter.PageIndex}" +
                            $"&pageSize={parameter.PageSize}" +
                            $"&searchMessage={parameter.SearchMessage}";
            return await client.ExecuteAsync<IPagedResult<TEntity>>(request);
        }

        public async Task<AjaxResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Post;
            request.Route = $"api/{ServiceName}";
            request.Parameter = entity;
            return await client.ExecuteAsync<TEntity>(request);
        }
    }
}
