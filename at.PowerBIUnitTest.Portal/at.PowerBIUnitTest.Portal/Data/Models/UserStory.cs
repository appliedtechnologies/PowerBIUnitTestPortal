using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class UserStory : ITrackModified
    {
        public int Id { get; set; }
        public Guid UniqueIdentifier { get; set; }

        public string Name { get; set; }
        public int TabularModel { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User ModifiedByNavigation { get; set; }

        public virtual ICollection<UnitTest> UnitTests { get; set; }
        public virtual TabularModel TabularModelNavigation { get; set; }
    }
}