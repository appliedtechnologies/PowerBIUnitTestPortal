using System;
using System.Collections.Generic;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class Tenant
    {
        public Tenant()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Guid MsId { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
