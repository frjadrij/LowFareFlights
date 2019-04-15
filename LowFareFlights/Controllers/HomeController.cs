using LowFareFlights.Bll.Services;
using LowFareFlights.Bll.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LowFareFlights.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFlightService _flightService;

        public HomeController(IFlightService flightService)
        {
            this._flightService = flightService;
        }

        public ActionResult Index()
        {
            return View(new FlightsVM());
        }

        [HttpPost]
        public async Task<ActionResult> SearchFlights(string origin, string destination, string departureDate, string returnDate, string adults, string currency)
        {
            FlightsVM viewModel = new FlightsVM();
            try
            {
                viewModel = await _flightService.SearchFlightsAsync(origin, destination, departureDate, returnDate, adults, currency);
            }
            catch(Exception ex)
            {

            }
            return PartialView("_FlightsList", viewModel);
        }
    }
}