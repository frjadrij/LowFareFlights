using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Bll.Models.JsonModels
{
    public class LowFareFlightsJson
    {
        [JsonProperty("data")]
        public List<DataJson> Data = new List<DataJson>();
        [JsonProperty("meta")]
        public MetaJson Meta = new MetaJson();
    }
}
