using Abp.Auditing;
using Revit.Configuration.Dto;

namespace Revit.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}