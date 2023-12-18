using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

public class Root
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }

        [JsonProperty("@odata.count")]
        public int OdataCount { get; set; }
        
        [JsonProperty("value")]
        public virtual ICollection<Value> Values { get; set; }
    }