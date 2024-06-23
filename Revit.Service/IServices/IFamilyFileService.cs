using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos.Family;
using Revit.Entity.Entity.Dtos.Project;
using Revit.Entity.Entity.Parameters;
using System.IO;
using System.Net.Http;

namespace Revit.Service.IServices
{
    public interface IFamilyFileService
    {
        Task<ApiResponse<PagedList<FamilyDto>>> GetAllAsync(QueryParameter parameter);
        Task<ApiResponse<byte[]>> DownLoadFamily(long familyId);
        Task<ApiResponse<IEnumerable<FamilyDto>>> UploadPublicAsync(UploadFileDtoBase parameter);
    }
}