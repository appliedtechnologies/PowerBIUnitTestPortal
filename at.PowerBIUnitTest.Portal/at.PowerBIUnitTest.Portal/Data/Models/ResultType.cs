using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class ResultType
    {   
        public int Id {get; set;}
        
        public string Name {get; set;}

        public virtual ICollection<UnitTest> UnitTests { get; set; }
    }
    
}