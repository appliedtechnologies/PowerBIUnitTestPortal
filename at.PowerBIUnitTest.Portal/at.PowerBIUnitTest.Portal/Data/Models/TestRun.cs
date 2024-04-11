using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class TestRun : ITrackModified
    {
        public int Id {get; set;}
        public DateTime TimeStamp {get; set;}
        public bool WasPassed {get; set;}
        public string Result {get; set;}
        public string ExpectedResult{get; set;}
        public int UnitTest {get; set;}
        public int TestRunCollection {get; set;}

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual User CreatedByNavigation { get; set; }
        public virtual User ModifiedByNavigation { get; set; }

        public virtual UnitTest UnitTestNavigation { get; set; }
        public virtual TestRunCollection TestRunCollectionNavigation { get; set; }
       
    }
}