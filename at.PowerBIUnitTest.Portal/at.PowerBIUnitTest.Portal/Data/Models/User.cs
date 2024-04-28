using System;
using System.Collections.Generic;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Guid MsId { get; set; }
        public int Tenant { get; set; }

        public virtual ICollection<UserStory> UserStoryCreatedByNavigations { get; set; }
        public virtual ICollection<UserStory> UserStoryModifiedByNavigations { get; set; }
        public virtual ICollection<UnitTest> UnitTestCreatedByNavigations { get; set; }
        public virtual ICollection<UnitTest> UnitTestModifiedByNavigations { get; set; }
        public virtual ICollection<TestRun> TestRunCreatedByNavigations { get; set; }
        public virtual ICollection<TestRun> TestRunModifiedByNavigations { get; set; }
        public virtual ICollection<TestRunCollection> TestRunCollectionCreatedByNavigations { get; set; }
        public virtual ICollection<TestRunCollection> TestRunCollectionModifiedByNavigations { get; set; }

        public virtual Tenant TenantNavigation { get; set; }
    }
}
