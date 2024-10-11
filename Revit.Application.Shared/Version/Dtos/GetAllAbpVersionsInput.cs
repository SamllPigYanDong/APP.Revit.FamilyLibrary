using Abp.Application.Services.Dto;
using System;

namespace Revit.Version.Dtos
{
    public class GetAllAbpVersionsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameFilter { get; set; }

    }
}