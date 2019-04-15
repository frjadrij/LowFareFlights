using LowFareFlights.Bll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Bll.Services
{
    public interface IFlightService
    {
        Task<FlightsVM> SearchFlightsAsync(string numOfPassangers, string departureAirport, string arrivalAirport, string departureTime, string returnTime, string valute);
    }
}
