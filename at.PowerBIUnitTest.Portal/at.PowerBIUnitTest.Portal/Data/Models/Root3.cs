using System.Collections.Generic;
using Newtonsoft.Json;


public class Root3
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }
        
        [JsonProperty("value")]
        public List<Value2> Values { get; set; }
    }