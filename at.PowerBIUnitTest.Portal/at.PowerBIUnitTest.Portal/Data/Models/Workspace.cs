using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class Workspace
    {
        public Workspace()
        {
            TabularModels = new HashSet<TabularModel>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string WorkspacePbId {get; set;}

        public virtual ICollection<TabularModel> TabularModels { get; set; }
        
    }
}