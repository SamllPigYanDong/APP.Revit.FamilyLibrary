using Revit.Accounts.Dto;
using Revit.Shared.Entity.Commons;

namespace Revit.Project.Dto
{
    public class ProjectResponseDto : DtoBase
    {

        public string ProjectName { get; set; }

        public AccountDto UserDto { get; set; }

        public int ProjectUserCount { get; set; }

        public double DocumentsSize { get; set; }

        public string BasePath { get; set; }


    }
}
