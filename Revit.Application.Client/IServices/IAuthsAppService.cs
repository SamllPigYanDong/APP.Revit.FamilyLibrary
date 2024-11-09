using System.Threading.Tasks;
using Abp.Web.Models;
using Revit.Accounts.Dto;
using Revit.ApiClient.Models;

namespace Revit.IServices
{
    public interface IAuthsAppService
    {
        Task LoginAsync();

    }
}
