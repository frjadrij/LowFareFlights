using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Bll.Models.JsonModels
{
    public class MetaJson
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("defaults")]
        public Defaults Defaults = new Defaults();
    }

    public class Defaults
    {
        [JsonProperty("nonStop")]
        public bool NonStop { get; set; }
        [JsonProperty("adults")]
        public int Adults { get; set; }
    }
}
