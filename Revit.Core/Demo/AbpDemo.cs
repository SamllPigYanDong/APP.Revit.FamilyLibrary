using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Demo
{
    [Table("AbpDemos")]
    public class AbpDemo : Entity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }

        public string Address { get; set; }
    }
}
