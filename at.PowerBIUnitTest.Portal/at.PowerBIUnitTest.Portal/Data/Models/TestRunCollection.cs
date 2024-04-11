using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class TestRunCollection : ITrackModified
    {
        public int Id {get; set;}
        public DateTime TimeStamp {get; set;}
        public string WasPassed {get; set;}

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User ModifiedByNavigation { get; set; }
        
        public virtual ICollection<TestRun> TestRuns { get; set; }
    }
}