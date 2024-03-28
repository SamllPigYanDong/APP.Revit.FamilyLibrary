using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Entity.Entity.Dtos
{
    public class DtoBase
    {
        public long Id { get; set; }

        public long CreatorId { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;

        public DateTime LastModificationTime { get; set; } = DateTime.Now;
    }
}
