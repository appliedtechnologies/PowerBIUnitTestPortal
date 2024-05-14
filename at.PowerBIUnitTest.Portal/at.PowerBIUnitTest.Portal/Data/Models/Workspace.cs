using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class Workspace
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid MsId { get; set; }

        public Guid UniqueIdentifier { get; set; }

        public bool IsVisible { get; set; }

        public int? Tenant { get; set; }

        public virtual Tenant TenantNavigation { get; set; }
        public virtual ICollection<TabularModel> TabularModels { get; set; }
    }
}