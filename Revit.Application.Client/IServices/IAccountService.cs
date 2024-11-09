using Revit.Accounts.Dto;
using Revit.Shared.Entity.Commons;
using System.Threading.Tasks;
using Abp.Web.Models;

namespace Revit.Service.IServices
{
    public interface IAccountService
    {
        Task<AjaxResponse<LoginedUserDto>> GetLoginedUser(string content);
    }
}