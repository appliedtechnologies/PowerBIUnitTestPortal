using System.Collections.Generic;

namespace at.PowerBIUnitTest.Portal.Data.Models
{
     public class QueryRequest
    {
        public class Query
        {
            public string query { get; set; }
        }

        public List<Query> queries { get; set; }

        public QueryRequest()
        {
        }

        public QueryRequest(string daxQuery)
        {
            queries = new List<Query> { new Query { query = daxQuery } };
        }
    }

    public class ParsedResponse
    {
        public class Result
        {
            public class Table
            {
                public List<Dictionary<string, object>> rows { get; set; }
            }
            public List<Table> tables { get; set; }
        }
        public List<Result> results { get; set; }
    }

}