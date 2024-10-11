using Revit.Entity.Entity;
using Revit.Entity.Entity.Parameters;
using Revit.Service.ApiServices;
using Revit.Service.IServices;
using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Commons.Page;
using System.Net.Http;
using System.Web.Routing;
using System.Windows.Forms;

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

        public async Task<ApiResponse<TEntity>> AddAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Post;
            request.Route = $"api/{ServiceName}";
            request.Parameter = entity;
            return await client.ExecuteAsync<TEntity>(request);
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Delete;
            request.Route = $"api/{ServiceName}/{id}";
            return await client.ExecuteAsync(request);
        }

        public async Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Get;
            request.Route = $"api/{ServiceName}?pageIndex={parameter.PageIndex}" +
                $"&pageSize={parameter.PageSize}" +
                $"&searchMessage={parameter.SearchMessage}";
            MessageBox.Show(request.Route);
            return await client.ExecuteAsync<PagedList<TEntity>>(request);
        }

        public async Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Get;
            request.Route = $"api/{ServiceName}?id={id}";
            return await client.ExecuteAsync<TEntity>(request);
        }

        public async Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Post;
            request.Route = $"api/{ServiceName}";
            request.Parameter = entity;
            return await client.ExecuteAsync<TEntity>(request);
        }
    }
}
