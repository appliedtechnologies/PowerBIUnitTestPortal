using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Value2
    {
        public string id { get; set; }
        public string name { get; set; }
        public string webUrl { get; set; }
        public bool addRowsAPIEnabled { get; set; }
        public string configuredBy { get; set; }
        public bool isRefreshable { get; set; }
        public bool isEffectiveIdentityRequired { get; set; }
        public bool isEffectiveIdentityRolesRequired { get; set; }
        public bool isOnPremGatewayRequired { get; set; }
        public string targetStorageMode { get; set; }
        public DateTime createdDate { get; set; }
        public string createReportEmbedURL { get; set; }
        public string qnaEmbedURL { get; set; }
        public List<object> upstreamDatasets { get; set; }
        public List<object> users { get; set; }
    }
