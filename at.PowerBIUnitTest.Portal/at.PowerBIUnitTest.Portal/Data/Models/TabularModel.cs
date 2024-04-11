using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace at.PowerBIUnitTest.Portal.Data.Models
{
    public partial class TabularModel
    {
        public int Id { get; set; }

        public Guid MsId { get; set; }

        public int Workspace {get; set;}

        public string Name {get; set;}

        public virtual ICollection<UserStory> UserStories { get; set; }

        public virtual Workspace WorkspaceNavigation { get; set; }     
    }
}