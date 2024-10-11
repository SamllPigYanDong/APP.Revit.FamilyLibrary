using Revit.Entity.Entity;
using Revit.Entity.Entity.Parameters;
using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Commons.Page;

namespace Revit.Service.IServices
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<ApiResponse<TEntity>> AddAsync(TEntity entity);

        Task<ApiResponse<TEntity>> UpdateAsync(TEntity entity);

        Task<ApiResponse> DeleteAsync(int id);

        Task<ApiResponse<TEntity>> GetFirstOfDefaultAsync(int id);

        Task<ApiResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter parameter);
    }
}
