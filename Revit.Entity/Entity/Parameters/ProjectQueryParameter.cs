using Revit.Entity.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Entity.Entity.Parameters
{
    public class ProjectQueryParameter: QueryParameter
    {
        public long UserId { get; set; }
    }
}
