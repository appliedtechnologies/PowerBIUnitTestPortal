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
    public partial class UnitTest
    {
        public UnitTest()
        {
           Histories = new HashSet<History>();
        }
        
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ExpectedResult { get; set; }

        public string LastResult { get; set; }

        public string Timestamp { get; set; }
        
        public int UserStory {get; set;}

        public string DAX {get; set;}

        public string ResultType {get; set;}

        public string DateTimeFormat {get; set;}
        
        public string DecimalPlaces {get; set;}

        public string FloatSeparators {get; set;}

        public virtual ICollection<History> Histories { get; set; }
        public virtual UserStory UserStoryNavigation { get; set; }
        public virtual ResultType ResultTypeNavigation {get; set;}

    }
}