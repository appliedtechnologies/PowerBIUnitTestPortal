using System;

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public interface ITrackModified : ITrackCreated
    {
        int ModifiedBy { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}