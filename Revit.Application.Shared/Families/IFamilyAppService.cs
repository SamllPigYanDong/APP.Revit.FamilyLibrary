using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Commons.Page;
using Revit.Shared.Entity.Family;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Revit.Families
{
    public interface IFamilyAppService
    {
        Task<FamilyDto> AuditingPublicAsync(FamilyPutDto parameter);
        Task<byte[]> DownLoadFamily(long familyId);
        Task<PagedList<FamilyDto>> GetPageListAsync(FamilyPageRequestDto parameter);
        Task<IEnumerable<FamilyDto>> GetUploadedFamilies(long userId);
        Task<IEnumerable<FamilyDto>> UploadPublicAsync(long creatorId, UploadFileDtoBase parameter);
    }
}