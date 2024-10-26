using Revit.ApiClient;
using Revit.ApiClient.Models;
using System.Threading.Tasks;

namespace Revit.Shared.Services.Storage
{
    public interface IAccountStorageService
    {
        Task StoreAccessTokenAsync(string newAccessToken);

        Task StoreAuthenticateResultAsync(AbpAuthenticateResultModel authenticateResultModel);

        AbpAuthenticateResultModel RetrieveAuthenticateResult();

        TenantInformation RetrieveTenantInfo();

        //GetCurrentLoginInformationsOutput RetrieveLoginInfo();

        void ClearSessionPersistance();

        //Task StoreLoginInformationAsync(GetCurrentLoginInformationsOutput loginInfo);

        Task StoreTenantInfoAsync(TenantInformation tenantInfo);
    }
}