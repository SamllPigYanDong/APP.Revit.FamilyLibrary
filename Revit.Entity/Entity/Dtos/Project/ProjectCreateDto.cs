using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Entity.Entity.Dtos.Project
{
    public class ProjectCreateDto:BaseDto
    {
        public string ProjectName { get; set; }

        public string ProjectAddress { get; set; }

        /// <summary>
        /// 项目介绍
        /// </summary>
        public string Introduction { get; set; }

        public string Icon { get; set; }
    }
}
