using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Bll.Models.JsonModels
{
    public class DataJson
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("offerItems")]
        public List<OfferItems> OfferItems = new List<OfferItems>();
    }

    public class OfferItems
    {
        [JsonProperty("price")]
        public Price Price { get; set; }
        [JsonProperty("services")]
        public List<Service> Services = new List<Service>();
    }

    public class Service
    {

        [JsonProperty("segments")]
        public List<Segment> Segments = new List<Segment>();
    }

    public class Price
    {
        [JsonProperty("total")]
        public string Total { get; set; }
        [JsonProperty("totalTaxes")]
        public string TotalTaxes { get; set; }
    }

    public class Segment
    {
        [JsonProperty("flightSegment")]
        public FlightSegment FlightSegment = new FlightSegment();

        [JsonProperty("pricingDetailPerAdult")]
        public PricingDetailPerAdult PricingDetailPerAdult = new PricingDetailPerAdult();
    }

    public class FlightSegment
    {
        [JsonProperty("carrierCode")]
        public string CarrierCode { get; set; }
        [JsonProperty("number")]
        public string FlightNumber { get; set; }
        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("departure")]
        public Departure Departure = new Departure();
        [JsonProperty("arrival")]
        public Arrival Arrival = new Arrival();
    }

    public class Departure
    {
        [JsonProperty("iataCode")]
        public string IataCode { get; set; }
        [JsonProperty("at")]
        public string TimeAt { get; set; }
    }

    public class Arrival
    {
        [JsonProperty("iataCode")]
        public string IataCode { get; set; }
        [JsonProperty("at")]
        public string TimeAt { get; set; }
    }

    public class PricingDetailPerAdult
    {
        [JsonProperty("travelClass")]
        public string TravelClass { get; set; }
        [JsonProperty("fareClass")]
        public string FareClass { get; set; }
        [JsonProperty("availability")]
        public int Availability { get; set; }
        [JsonProperty("fareBasis")]
        public string FareBasis { get; set; }
    }
}
