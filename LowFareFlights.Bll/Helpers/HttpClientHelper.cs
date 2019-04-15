using LowFareFlights.Bll.Models.JsonModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LowFareFlights.Bll.Helpers
{
    public static class HttpClientHelper
    {
        public const string HttpMethodPost = "POST";
        public const string TokenURI = "TokenURI";
        public const string TokenParams = "TokenParams";

        public const string GrantType = "grant_type";
        public const string ClientId = "client_id";
        public const string ClientSecret = "client_secret";

        public const string URI = "URI";
        public const string Token = "Bearer";

        public const string Origin = "origin";
        public const string Destination = "destination";
        public const string DepartureDate = "departureDate";
        public const string ReturnDate = "returnDate";
        public const string Adults = "adults";
        public const string Currency = "currency";
        public const string MaxOffers = "max";


        public static HttpRequestMessage InitializeRequestMessageForToken()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod(HttpMethodPost), $"{ConfigurationManager.AppSettings[TokenURI]}/?{ConfigurationManager.AppSettings[TokenParams]}");
            var requestBody = new Dictionary<string, string>() { { GrantType, ConfigurationManager.AppSettings[GrantType] }, { ClientId, ConfigurationManager.AppSettings[ClientId] }, { ClientSecret, ConfigurationManager.AppSettings[ClientSecret] } };
            requestMessage.Content = new FormUrlEncodedContent(requestBody);
            
            return requestMessage;
        }


        public static HttpClient InitializeClientForFlightsFetching(string accessToken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token, accessToken);
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings[URI]);
         
            return httpClient;
        }


        public static string InitializeParamsQueryForClient(string origin, string destination, string departureDate, string returnDate, string adults, string currency)
        {
            //origin = "NYC";
            //destination = "MAD";

            DateTime departureDateDateTime = Convert.ToDateTime(departureDate);
            departureDate = departureDateDateTime.ToString("yyyy-MM-dd");

            if (!String.IsNullOrEmpty(returnDate))
            { 
                DateTime returnDateDateTime = Convert.ToDateTime(returnDate);
                returnDate = departureDateDateTime.ToString("yyyy-MM-dd");
            }

            //adults = "5";
            //currency = "USD";

            var paramsDictionary = new Dictionary<string, string>();

            paramsDictionary.Add(Origin, origin);
            paramsDictionary.Add(Destination, destination);
            paramsDictionary.Add(DepartureDate, departureDate);
            if (!String.IsNullOrEmpty(returnDate))
                paramsDictionary.Add(ReturnDate, returnDate);
            paramsDictionary.Add(Adults, adults);
            if (!String.IsNullOrEmpty(currency))
                paramsDictionary.Add(Currency, currency);
            //paramsDictionary.Add(MaxOffers, ConfigurationManager.AppSettings[MaxOffers]);

            var paramsUrlEncoded = new FormUrlEncodedContent(paramsDictionary);
            var paramsQuery = paramsUrlEncoded.ReadAsStringAsync().Result;
            paramsQuery = paramsQuery.Insert(0, "?");

            return paramsQuery;
        }

    }
}
