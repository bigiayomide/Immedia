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

        //public Customer Get(int id)
        //{
        //    var response = _client.Execute<Customer>(new RestRequest(string.Format("search/Get/?apikey={1}&companyid={2}",  _apiKey, _companyId), Method.GET));
        //    return response.Data;
        //}
    }
}
