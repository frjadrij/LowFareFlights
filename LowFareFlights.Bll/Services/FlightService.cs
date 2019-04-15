using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LowFareFlights.Bll.Helpers;
using LowFareFlights.Bll.Mappers;
using LowFareFlights.Bll.Models.JsonModels;
using LowFareFlights.Bll.ViewModels;
using LowFareFlights.Dal.UnitOfWork;
using LowFareFlights.Entity.Entities;
using Newtonsoft.Json;

namespace LowFareFlights.Bll.Services
{
    public class FlightService : IFlightService
    {
        const string Errors = "errors";
        static HttpClient httpClient;

        private readonly IUnitOfWork _unitOfWork;

        public FlightService() : this(new UnitOfWork()) { }

        public FlightService(IUnitOfWork unitOfWorkManager)
        {
            this._unitOfWork = unitOfWorkManager;
        }

        public async Task<FlightsVM> SearchFlightsAsync(string origin, string destination, string departureDate, string returnDate, string adults, string currency)
        {
            FlightsVM viewModel = new FlightsVM();
            try
            {
                viewModel = CheckIfFlightsExistInDatabase(origin, destination, departureDate, returnDate, adults, currency);
                if (viewModel.Flights.Count() != 0)
                    return viewModel;

                var bearerToken = await RequestBearerTokenAsync();
                var responseJson = await RequestFlightsDataAsync(bearerToken, origin, destination, departureDate, returnDate, adults, currency);
                if (responseJson.Contains(Errors))
                {
                    var errorJsonModel = JsonConvert.DeserializeObject<ErrorFromJson>(responseJson);
                    viewModel = errorJsonModel.MapToViewModel();
                }
                else
                {
                    var lowFareFlightsJsonModel = JsonConvert.DeserializeObject<LowFareFlightsJson>(responseJson);
                    viewModel = lowFareFlightsJsonModel.MapToViewModel(adults);

                    await SaveFlightsToDb(viewModel);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return viewModel;
        }


        private async Task<BearerTokenFromJson> RequestBearerTokenAsync()
        {
            BearerTokenFromJson bearerToken = new BearerTokenFromJson();
            try
            {
                HttpRequestMessage httpRequestMessage = HttpClientHelper.InitializeRequestMessageForToken();
                using (httpClient = new HttpClient())
                {
                    HttpResponseMessage response = httpClient.SendAsync(httpRequestMessage).Result;

                    var responseJson = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();
                    bearerToken = JsonConvert.DeserializeObject<BearerTokenFromJson>(responseJson);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return bearerToken;
        }


        private async Task<string> RequestFlightsDataAsync(BearerTokenFromJson bearerToken, string origin, string destination, string departureDate, string returnDate, string adults, string currency)
        {
            string responseString = string.Empty;
            try
            {
                httpClient = HttpClientHelper.InitializeClientForFlightsFetching(bearerToken.AccessToken);
                var paramsQuery = HttpClientHelper.InitializeParamsQueryForClient(origin, destination, departureDate, returnDate, adults, currency);

                using (HttpResponseMessage response = await httpClient.GetAsync(paramsQuery))
                { 
                    responseString = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return responseString;
        }


        private FlightsVM CheckIfFlightsExistInDatabase(string origin, string destination, string departureDate, string returnDate, string adults, string currency)
        {
            FlightsVM flightsVm = new FlightsVM();

            var existingFlights = _unitOfWork.Flights.GetAllAsQueryableAsNoTracking().ToList();
            if (existingFlights != null || existingFlights.Count() != 0)
            {
                var flightsForView = existingFlights.Where(f => f.NumberOfPassangers == Convert.ToInt32(adults)
                            && f.OriginAirportIATA == origin
                            && f.DestinationAirportIATA == destination
                            && f.OriginDepartureTime.ToString("MM/dd/yyyy").Equals(departureDate)
                            && f.OriginArrivalTime.ToString("MM/dd/yyyy").Equals(returnDate)
                            && f.Currency == currency
                    ).ToList();

                flightsVm.Flights = flightsForView.MapListToViewModel();
            }

            return flightsVm;
        }


        private async Task SaveFlightsToDb(FlightsVM viewModel)
        {
            try
            {
                var existingFlights = _unitOfWork.Flights.GetAllAsQueryable().ToList();
                var flightsToSave = viewModel.Flights;

                var flightsNotToSave = flightsToSave
                    .Where(f => existingFlights.Select(fl => fl.NumberOfPassangers).Contains(f.NumberOfPassangers)

                    && existingFlights.Select(fl => fl.OriginAirportIATA).Contains(f.OriginDepartureAirport)
                    && existingFlights.Select(fl => fl.DestinationAirportIATA).Contains(f.DestinationArrivalAirport)

                    && existingFlights.Select(fl => fl.DestinationAirportIATA).Contains(f.DestinationDepartureAirport)
                    && existingFlights.Select(fl => fl.OriginAirportIATA).Contains(f.OriginArrivalAirport)

                    && existingFlights.Select(fl => fl.OriginArrivalLayovers).Contains(f.OriginArrivalLayovers)
                    && existingFlights.Select(fl => fl.DestinationArrivalLayovers).Contains(f.DestinationArrivalLayovers)

                    && existingFlights.Select(fl => fl.TotalPrice).Contains(f.TotalPrice)
                    && existingFlights.Select(fl => fl.Currency).Contains(f.Currency)
                    ).ToList();

                var flightsToDbSave = flightsToSave.Except(flightsNotToSave).ToList();


                if (viewModel.Flights != null && viewModel.Flights.Count != 0)
                {
                    var flightsDbModels = viewModel.Flights.MapListToModel();
                    _unitOfWork.Flights.AddRangeToDatabase(flightsDbModels);

                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
