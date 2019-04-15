using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Entity.Entities
{
    public class Flight
    {
        public Guid Id { get; set; }

        public int NumberOfPassangers { get; set; }

        public string OriginAirportIATA { get; set; }
        public string DestinationAirportIATA { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime OriginDepartureTime { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DestinationArrivalTime { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DestinationDepartureTime { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime OriginArrivalTime { get; set; }

        public int DestinationArrivalLayovers { get; set; }
        public int OriginArrivalLayovers { get; set; }

        public string TotalPrice { get; set; }
        public string Currency { get; set; }
    }
}
