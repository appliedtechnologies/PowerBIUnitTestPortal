using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class Report
    {
        [Key]
        public int Id { get; set; }
        public Guid ReportId { get; set; }

        public Guid WorkspaceId { get; set; }

        public int? Tenant { get; set; }

        public string Name { get; set; }

        public virtual Tenant TenantNavigation { get; set; }

    }
}