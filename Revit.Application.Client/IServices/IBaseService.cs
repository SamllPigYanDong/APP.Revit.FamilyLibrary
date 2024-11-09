using Abp.Application.Services.Dto;
using Revit.Commons;
using Revit.Shared.Entity.Commons;
using System.Threading.Tasks;
using Abp.Web.Models;

namespace Revit.Service.IServices
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<AjaxResponse<TEntity>> AddAsync(TEntity entity);

        Task<AjaxResponse<TEntity>> UpdateAsync(TEntity entity);

        Task<AjaxResponse> DeleteAsync(int id);

        Task<AjaxResponse<TEntity>> GetFirstOfDefaultAsync(int id);

        Task<AjaxResponse<IPagedResult<TEntity>>> GetListAsync(QueryParameter parameter);
    }
}
