using System;

namespace Revit.MultiTenancy.HostDashboard.Dto
{
    public class RecentTenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
    }
}