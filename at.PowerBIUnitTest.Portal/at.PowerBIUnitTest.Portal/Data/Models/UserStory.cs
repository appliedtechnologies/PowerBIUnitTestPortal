using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class UserStory
    {
        public UserStory()
        {
           UnitTests = new HashSet<UnitTest>();
        }

        [Key]
        public int Id { get; set; }

        public string Beschreibung { get; set; }

        public int TabularModel { get; set; }

        public virtual ICollection<UnitTest> UnitTests { get; set; }

        public virtual TabularModel TabularModelNavigation { get; set; }
    }
}