using Immedia.Picture.Api.Request.Requests;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immedia.Picture.Api.Request
{
    public class ApiRequest
    {
        private readonly string _apiKey;
        private readonly IRestClient _client;

        public SearchRequest CustomerRequest { get { return new SearchRequest(_client, _apiKey); } }

        public ApiRequest(string username, string password, string apiKey, int companyId)
        {
            _apiKey = apiKey;

            _client = new RestClient
            {
                BaseUrl = new Uri("https://api.foursquare.com/v2/venues/"),
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
        }
    }
}
