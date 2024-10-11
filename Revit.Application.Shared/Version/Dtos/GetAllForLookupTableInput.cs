using Abp.Application.Services.Dto;

namespace Revit.Version.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}