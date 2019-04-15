using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LowFareFlights.Bll.ViewModels
{
    public class FlightsVM
    {
        public string NumberOfPassangers { get; set; }
        public string OriginAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string Currency { get; set; }
        public string OriginDepartureDate { get; set; }
        public string OriginArrivalDate { get; set; }

        public List<string> ErrorMessages = new List<string>();

        public List<FlightVM> Flights = new List<FlightVM>();
    }

    public class FlightVM
    {
        public int NumberOfPassangers { get; set; }

        public string OriginDepartureAirport { get; set; }
        public DateTime OriginDepartureTime { get; set; }

        public string DestinationArrivalAirport { get; set; }
        public DateTime DestinationArrivalTime { get; set; }

        public int DestinationArrivalLayovers { get; set; }

        public string DestinationDepartureAirport { get; set; }
        public DateTime DestinationDepartureTime { get; set; }

        public string OriginArrivalAirport { get; set; } 
        public DateTime OriginArrivalTime { get; set; }

        public int OriginArrivalLayovers { get; set; }

        public string TotalPrice { get; set; }
        public string Currency { get; set; }
    }
}
