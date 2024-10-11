using Revit.Shared.Entity.Commons.Page;

namespace Revit.Project.Dto
{
    public class ProjectPageRequestDto : PageRequestDto
    {
        //删除
        public long UserId { get; set; }

    }
}
