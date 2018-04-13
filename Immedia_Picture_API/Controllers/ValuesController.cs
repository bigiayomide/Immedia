using Immedia.Picture.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Immedia.Picture.Api.Controllers
{
    //[Authorize]
    [RoutePrefix("values")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [Route("GetLocationPictures")]
        public IHttpActionResult GetLocationPicturesAsync(string lon, string lat, int? page)
        {

            try
            {
                Result result = new Result() { Page = 1, Pages = 2 };
                //Result result = await _searchRequest.GetPhotosforLocationAsync(lat, lon, page.Value);
                if (result != null)
                {
                    //_IUserRepository = _IDataRepositoryFactory.GetDataRepository<IUserRepository>();
                    return Content(HttpStatusCode.OK, result);
                }
                else
                {

                    return Content(HttpStatusCode.InternalServerError, result);
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);

            }

        }
    }
}
