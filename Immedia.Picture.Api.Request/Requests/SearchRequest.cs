using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immedia.Picture.Api.Request.Requests
{
    public class SearchRequest: RequestBase
    {
        public SearchRequest(IRestClient client, string apiKey) : base(client, apiKey) { }

        public Photos GetPhotos(string lat, string lon, int page=1)
        {
            var response = _client.Execute<Photos>(new RestRequest(string.Format("&apikey={0}&lat={1}&lon={2}&page={3}", _apiKey, lat, lon,page), Method.GET));
            return response.Data;
        }
    }
}
