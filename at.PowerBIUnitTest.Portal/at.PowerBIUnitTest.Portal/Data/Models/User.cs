using System;
using System.Collections.Generic;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Guid MsId { get; set; }
        public int Tenant { get; set; }

        public virtual Tenant TenantNavigation { get; set; }
    }
}
