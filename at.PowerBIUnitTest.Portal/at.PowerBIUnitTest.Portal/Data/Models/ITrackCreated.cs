using System;

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public interface ITrackCreated
    {
        int? CreatedBy { get; set; }
        DateTime? CreatedOn { get; set; }
    }
}