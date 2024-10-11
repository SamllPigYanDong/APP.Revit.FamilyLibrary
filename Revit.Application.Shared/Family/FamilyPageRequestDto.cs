using Revit.Shared.Entity.Commons.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Shared.Entity.Family
{
    /// <summary>
    /// 角色翻页查询
    /// </summary>
    public class FamilyPageRequestDto : PageRequestDto
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Name { get; set; }


        public List<long> CategoriesIds { get; set; }


        public FamilyAuditStatus AuditStatus { get; set; }
    }
}
