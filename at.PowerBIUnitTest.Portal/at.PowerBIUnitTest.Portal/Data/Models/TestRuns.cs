using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class TestRuns
    {
          public TestRuns()
        {
           HistoriesRun = new HashSet<History>();
        }

        [Key]
        public int Id {get; set;}
        public string TimeStamp {get; set;}
        public string Result {get; set;}
        public string Workspace {get; set;}
        public string TabularModel {get; set;}
        public string UserStory {get; set;}
        public int Count {get; set;}
        public string Type {get; set;}
        
        public virtual ICollection<History> HistoriesRun { get; set; }
        //public virtual UnitTest UnitTestNavigation { get; set; }
        
    }
}