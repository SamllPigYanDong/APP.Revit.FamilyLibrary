using Revit.Authorization.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Authorization.Roles.Dto
{
    /// <summary>
    /// 角色信息
    /// </summary>
    public class RoleDto
    {
        public long Id { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public UserDto Creator { get; set; } = null;

        public long CreatorId { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;

        public DateTime LastModificationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态，0：禁用，1：正常
        /// </summary>
        public RoleStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
