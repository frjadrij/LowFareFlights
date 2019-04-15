using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Bll.Models.JsonModels
{
    public class BearerTokenFromJson
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
