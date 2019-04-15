using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Bll.ViewModels
{
    public class AirportVM
    {
        public Guid Id { get; set; }
        public string IATA { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string TimeZone { get; set; }
    }
}
