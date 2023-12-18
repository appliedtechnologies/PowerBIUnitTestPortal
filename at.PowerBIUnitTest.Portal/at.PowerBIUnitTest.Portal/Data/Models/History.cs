using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class History
    {
        
        public int Id {get; set;}
        public string TimeStamp {get; set;}
        public string Result {get; set;}
        public string LastRun {get; set;}
        public string ExpectedRun{get; set;}
        public int UnitTest {get; set;}
        public int? TestRun {get; set;}
        

        public virtual UnitTest UnitTestNavigation { get; set; }
        public virtual TestRuns TestRunNavigation { get; set; }
       
    }
}