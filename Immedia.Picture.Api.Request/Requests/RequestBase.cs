using RestSharp;
using System.Net;

namespace Immedia.Picture.Api.Request.Requests
{
    public class RequestBase
    {
        protected readonly IRestClient _client;
        protected readonly string _apiKey;

        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }

        public RequestBase() { }

        public RequestBase(IRestClient client, string apiKey)
        {
            _client = client;
            _apiKey = apiKey;
        }

    }
}