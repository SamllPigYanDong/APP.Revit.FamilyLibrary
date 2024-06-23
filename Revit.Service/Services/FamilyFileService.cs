using Autodesk.Revit.DB.ExtensibleStorage;
using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos.Family;
using Revit.Entity.Entity.Dtos.Project;
using Revit.Entity.Entity.Parameters;
using Revit.Service.ApiServices;
using Revit.Service.IServices;
using Revit.Service.Services;
using System.Net.Http;
using System.Windows.Forms;

namespace Revit.Application.ViewModels.FamilyViewModels
{
    public class FamilyFileService : BaseService<FamilyDto>, IFamilyFileService
    {
        public FamilyFileService(MyHttpClient client) : base(client)
        {
            ServiceName = "Family";
        }

        public async Task<ApiResponse<PagedList<FamilyDto>>> GetAllAsync(QueryParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Get;
            request.Route = $"api/{ServiceName}?pageIndex={parameter.PageIndex}" +
                $"&pageSize={parameter.PageSize}" +
                $"&searchMessage={parameter.SearchMessage}";
            return await client.ExecuteAsync<PagedList<FamilyDto>>(request);
        }

        public async Task<ApiResponse<IEnumerable<FamilyDto>>> UploadPublicAsync(UploadFileDtoBase parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Post;
            request.Route = $"api/{ServiceName}";
            request.FilePaths = parameter.Files;
            return await client.ExecuteAsync<IEnumerable<FamilyDto>>(request);
        }


        public async Task<ApiResponse<byte[]>> DownLoadFamily(long familyId)
        {
            BaseRequest request = new BaseRequest();
            request.Method = HttpMethod.Get;
            request.Route = $"api/{ServiceName}/{familyId}";
            return await client.ExecuteAsync<byte[]>(request);
        }
    }
}