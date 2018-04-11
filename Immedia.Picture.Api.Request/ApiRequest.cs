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

        public SearchRequest SearchRequest { get { return new SearchRequest(_client, _apiKey); } }

        public ApiRequest(string apiKey)
        {
            _apiKey = apiKey;

            _client = new RestClient
            {
                BaseUrl = new Uri("https://api.flickr.com/services/rest/?method=flickr.photos.search&format=json&nojsoncallback=1"),
            };
        }
    }
}
