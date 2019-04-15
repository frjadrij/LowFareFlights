using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Bll.Models.JsonModels
{
    public class ErrorFromJson
    {
        [JsonProperty("errors")]
        public List<Error> Errors = new List<Error>();
    }

    public class Error
    {
        [JsonProperty("errors")]
        public int Status { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("detail")]
        public string Detail { get; set; }
        [JsonProperty("source")]
        public Source Source = new Source();
    }

    public class Source
    {
        [JsonProperty("parameter")]
        public string Parameter { get; set; }
    }
}
