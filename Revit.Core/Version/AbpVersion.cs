using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace AppFramework.Version
{
    [Table("AbpVersions")]
    public class AbpVersion : Entity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Version { get; set; }

        public virtual string DownloadUrl { get; set; }

        public virtual string ChangelogUrl { get; set; }

        public virtual string MinimumVersion { get; set; }

        public virtual string AlgorithmValue { get; set; }

        public virtual string HashingAlgorithm { get; set; }

        public virtual bool IsEnable { get; set; }

        public virtual bool IsForced { get; set; }

    }
}