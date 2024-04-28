using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Data.Entity.Infrastructure.Annotations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class UnitTest : ITrackModified
    {
        public int Id { get; set; }
        public Guid UniqueIdentifier { get; set; }

        public string Name { get; set; }
        public string ExpectedResult { get; set; }
        public int UserStory { get; set; }
        public string DAX { get; set; }
        public string ResultType { get; set; }
        public string DateTimeFormat { get; set; }
        public string DecimalPlaces { get; set; }
        public string FloatSeparators { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User ModifiedByNavigation { get; set; }

        public virtual ICollection<TestRun> TestRuns { get; set; }
        public virtual UserStory UserStoryNavigation { get; set; }
    }
}