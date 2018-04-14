using Immedia.Picture.Api.Entities;
using Immedia.Picture.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immedia.Picture.Api.Request.Requests
{
    public class SearchRequest: RequestBase, ISearchRequest
    {
        public SearchRequest(IRestClient client, string apiKey) : base(client, apiKey) { }

        public async Task<Result> GetPhotosforLocationAsync(string lat, string lon, int page = 1)
        {
            IRestResponse<Result> response;
            return await Task.Factory.StartNew(() =>
            {
                response = _client.Execute<Result>(new RestRequest(string.Format("?method=flickr.photos.search&format=rest&accuracy=11&api_key={0}&lat={1}&lon={2}&page={3}", _apiKey, lat, lon, page), Method.GET));
                
                return response.Data;
            });
        }
        public async Task<Photo> GetPhotoDetailsAsync(int id)
        {
            return await Task.Factory.StartNew(() =>
            {
                IRestResponse<Photo> response = _client.Execute<Photo>(new RestRequest(string.Format("?method=flickr.photos.getInfo&format=rest&api_key={0}&photo_id={1}", _apiKey, id), Method.GET));
                return response.Data;
            });
        }

        public async Task<List<Place>> GetLocationquery(string query)
        {
            return await Task.Run(() =>
            {
                RestRequest request = new RestRequest(string.Format("?method=flickr.places.find&format=rest&api_key={0}&query={1}", _apiKey, query), Method.GET);
                IRestResponse<List<Place>> response = _client.Execute<List<Place>>(request);
                return response.Data;
            });
        }

    }
}
