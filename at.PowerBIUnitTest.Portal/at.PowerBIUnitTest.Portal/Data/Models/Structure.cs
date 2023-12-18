using System;
using System.Collections.Generic;

namespace at.PowerBIUnitTest.Portal.Data.Models{
public class Structur
    {
        public string Typ { get; set; }
        public string Name { get; set; }
        public string WorkspacePbId { get; set; }
        public string DatasetPbId { get; set; }
        public string ExpectedResult { get; set; }
        public string DAX { get; set; }
        public string Timestamp { get; set; }
        public string LastResult { get; set; }
        public int UserStory { get; set; }
        public int Id { get; set; }
        public List<Structur> items { get; set; }
        
        public virtual UserStory UserStoryNavigation { get; set; }
    }
}