using LowFareFlights.Bll.Models.JsonModels;
using LowFareFlights.Bll.ViewModels;
using LowFareFlights.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Bll.Mappers
{
    public static class FlightMapper
    {
        public static Flight MapToModel(this FlightVM viewModel, Flight model = null)
        {
            if (viewModel == null)
                return null;

            if (model == null)
                model = new Flight { Id = Guid.NewGuid() };

            // fields
            model.NumberOfPassangers = viewModel.NumberOfPassangers;

            model.OriginAirportIATA = viewModel.OriginDepartureAirport;
            model.DestinationAirportIATA = viewModel.DestinationArrivalAirport;

            model.OriginDepartureTime = viewModel.OriginDepartureTime;
            model.DestinationArrivalTime = viewModel.DestinationArrivalTime;

            model.DestinationDepartureTime = viewModel.DestinationDepartureTime;
            model.OriginArrivalTime = viewModel.OriginArrivalTime;

            model.OriginArrivalLayovers = viewModel.OriginArrivalLayovers;
            model.DestinationArrivalLayovers = viewModel.DestinationArrivalLayovers;

            model.TotalPrice = viewModel.TotalPrice;
            model.Currency = viewModel.Currency;

            // relations

            return model;
        }


        public static List<Flight> MapListToModel(this List<FlightVM> viewModels, List<Flight> dbModel = null)
        {
            List<Flight> model = new List<Flight>();

            if (viewModels == null)
                return model;

            if (dbModel == null)
            {
                foreach (var item in viewModels)
                    model.Add(item.MapToModel());
            }
            else
            {
                foreach (var item in viewModels)
                {
                    //Flight dbitem = dbModel.FirstOrDefault(i => i.OfferId == item.OfferId);
                    //if (dbitem != null)
                    //    model.Add(item.MapToModel(dbitem));
                    //else
                    model.Add(item.MapToModel());
                }
            }

            return model;
        }


        public static FlightVM MapToViewModel(this Flight model)
        {
            if (model == null)
                return null;

            return new FlightVM()
            {
                NumberOfPassangers = model.NumberOfPassangers,

                OriginDepartureAirport = model.OriginAirportIATA,
                DestinationArrivalAirport = model.DestinationAirportIATA,
    
                DestinationDepartureAirport = model.DestinationAirportIATA,
                OriginArrivalAirport = model.OriginAirportIATA,

                OriginDepartureTime = model.OriginDepartureTime,
                DestinationArrivalTime = model.DestinationArrivalTime,

                DestinationDepartureTime = model.DestinationDepartureTime,
                OriginArrivalTime = model.OriginArrivalTime,

                OriginArrivalLayovers = model.OriginArrivalLayovers,
                DestinationArrivalLayovers = model.DestinationArrivalLayovers,

                TotalPrice = model.TotalPrice,
                Currency = model.Currency
            };
        }

        public static List<FlightVM> MapListToViewModel(this List<Flight> models)
        {
            if (!models.Any())
                return new List<FlightVM>();

            List<FlightVM> returnList = new List<FlightVM>();
            foreach (var model in models)
                returnList.Add(model.MapToViewModel());

            return returnList;
        }





        public static FlightsVM MapToViewModel(this ErrorFromJson model)
        {
            if (model == null)
                return null;

            FlightsVM flightsVM = new FlightsVM();
            foreach (var error in model.Errors)
                flightsVM.ErrorMessages.Add(error.Detail + error.Source.Parameter);

            return flightsVM;
        }

        public static FlightsVM MapToViewModel(this LowFareFlightsJson model, string adults)
        {
            if (model == null)
                return null;

            FlightsVM flightsVM = new FlightsVM();

            foreach (var flightOffer in model.Data)
            {
                FlightVM flightVM = new FlightVM();

                if (model.Meta.Defaults.Adults == 0)
                    flightVM.NumberOfPassangers = Convert.ToInt32(adults);
                else
                    flightVM.NumberOfPassangers = model.Meta.Defaults.Adults;

                foreach(var offerItem in flightOffer.OfferItems)
                { 
       
                    var totalPrice = offerItem.Price.Total;
                    flightVM.TotalPrice = totalPrice;
                    flightVM.Currency = model.Meta.Currency;

                    var services = offerItem.Services;

                    // Origin segment
                    var originSegment = services.First();
                    var numberOfOriginLayovers = originSegment.Segments.Count() - 1;
                    flightVM.DestinationArrivalLayovers = numberOfOriginLayovers;

                    var originDepartureAirport = originSegment.Segments.First().FlightSegment.Departure.IataCode;
                    flightVM.OriginDepartureAirport = originDepartureAirport;

                    var originDepartureTime = originSegment.Segments.First().FlightSegment.Departure.TimeAt;
                    flightVM.OriginDepartureTime = Convert.ToDateTime(originDepartureTime);

                    var destinationArrivalAirport = originSegment.Segments.Last().FlightSegment.Arrival.IataCode;
                    flightVM.DestinationArrivalAirport = destinationArrivalAirport;

                    var destinationArrivalTime = originSegment.Segments.Last().FlightSegment.Arrival.TimeAt;
                    flightVM.DestinationArrivalTime = Convert.ToDateTime(destinationArrivalTime);

                    var returnSegment = services.Last();
                    var numberOfOriginReturnLayovers = returnSegment.Segments.Count() - 1;
                    flightVM.OriginArrivalLayovers = numberOfOriginReturnLayovers;

                    var destinationReturnDepartureAirport = returnSegment.Segments.First().FlightSegment.Departure.IataCode;
                    flightVM.DestinationDepartureAirport = destinationReturnDepartureAirport;

                    var destinationReturnDepartureTime = returnSegment.Segments.First().FlightSegment.Departure.TimeAt;
                    flightVM.DestinationDepartureTime = Convert.ToDateTime(destinationReturnDepartureTime);

                    var originReturnArrivalAirport = returnSegment.Segments.Last().FlightSegment.Arrival.IataCode;
                    flightVM.OriginArrivalAirport = originReturnArrivalAirport;

                    var originReturnArrivalTime = returnSegment.Segments.Last().FlightSegment.Arrival.TimeAt;
                    flightVM.OriginArrivalTime = Convert.ToDateTime(originReturnArrivalTime);

                    flightsVM.Flights.Add(flightVM);
                }
            }

            return flightsVM;
        }
    }
}
