using Revit.Commons;
using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Commons.Page;
using System.Threading.Tasks;

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
