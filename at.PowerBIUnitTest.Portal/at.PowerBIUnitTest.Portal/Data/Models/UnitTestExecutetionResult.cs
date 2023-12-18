using Microsoft.AspNetCore.OData.Routing.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace at.PowerBIUnitTest.Portal.Data.Models {

public class UnitTestExecutionResult{

    public bool UnitTestSucceeded { get; set; }
    public bool UnitTestExecuted { get; set; }

}

}