using Immedia.Picture.Api.Request;
using Immedia.Picture.Api.Request.Requests;
using Immedia.Picture.Data;
using System.Threading.Tasks;
using System.Web.Http;

namespace Immedia_Picture_API.Controllers
{
    public class PictureController : PictureApiController
    {
        ISearchRequest _searchRequest;
        public PictureController()
        {
            Init();
            _searchRequest = Api.SearchRequest;
        }
        // GET: Picture

        public async Task<IHttpActionResult> GetLocationPicturesAsync(string lon, string lat, int page)
        {
            Result result = await _searchRequest.GetPhotosforLocationAsync(lat, lon, page);
            if (result != null)
            {
                return Ok(result);
            }
            else
                return NotFound();
        }
    }
}