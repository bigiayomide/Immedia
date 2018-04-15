using Immedia.Picture.Api.Core.Common.Core;
using Immedia.Picture.Api.Core.Contracts;
using Immedia.Picture.Api.Entities;
using Immedia.Picture.Business;
using Immedia.Picture.Business.Interface;
using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Immedia_Picture_API.Controllers
{

    [RoutePrefix("api/Picture")]
    public class PictureController : ApiController
    {
        [Import]
        IBusinessEngineFactory _BusinessRepositoryFactory;
        IBusinessEngine business;
        public PictureController()
        {
            business=ObjectBase.Container.GetExportedValue<IBusinessEngine>();
        }
        [ImportingConstructor]
        public PictureController(IBusinessEngineFactory BusinessRepositoryFactory)
        {
            _BusinessRepositoryFactory = BusinessRepositoryFactory;
        }



        [Route("GetLocationPicturesById")]
        [HttpPost]
        public async   Task<IHttpActionResult> PostLocationPicturesById(Place place)
        {
            int? page = 1;
            try
            {
                return Content(HttpStatusCode.OK, await business.GetLocationPicturesByIdAsync(place, page.Value, User.Identity.GetUserId()));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "");
            }
        }
        [Route("GetLocationByLonLat")]
        public async Task<IHttpActionResult> GetLocationByLonLat(string longitide,string latitude,int? page)
        {
            try
            {
                return Content(HttpStatusCode.OK, await business.GetLocationByLonLat(longitide,latitude,page));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [Route("SavePictureforUser")]
        [Authorize]
        public IHttpActionResult SavePictureforUser(Photo photo)
        {
            try
            {

                business.SavePictureforUser(photo,User.Identity.GetUserId());
                return Ok();
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError,ex.Message);
            }
        }
        [Route("Getlocations")]
        public async Task<IHttpActionResult> Getlocations(string query)
        {
            try
            {
                return Content(HttpStatusCode.OK, await business.Getlocations(query));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public IHttpActionResult GetPictureDetails(int Id )
        {
            try
            {
                return Content(HttpStatusCode.OK, business.GetPictureDetails(Id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}