using Immedia.Picture.Api.Core.Contracts;
using Immedia.Picture.Api.Entities;
using Immedia.Picture.Api.Request;
using Immedia.Picture.Api.Request.Requests;
using Immedia.Picture.Business;
using Immedia.Picture.Business.Interface;
using Immedia.Picture.Data;
using Immedia.Picture.Data.Interface;
using Immedia.Picture.Data.Repository;
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

        public PictureController()
        {
        }
        [Import]
        IBusinessEngineFactory _BusinessRepositoryFactory;

        [Route("GetLocationPictures")]
        public async  Task<IHttpActionResult> GetLonLatPictures(Place place,int? page)
        {
            try
            {
                IBusinessEngine business =  _BusinessRepositoryFactory.GetBusinessEngine<IBusinessEngine>();
                return Content(HttpStatusCode.OK, await business.GetLocationPictureLatLonAsync(place, page.Value, User.Identity.GetUserId()));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public IHttpActionResult SavePictureforUser(string userid, Photo photo)
        {
            try
            {
                IBusinessEngine business = _BusinessRepositoryFactory.GetBusinessEngine<IBusinessEngine>();
                business.SavePictureforUser(userid, photo);
                return Ok();
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError,ex.Message);
            }
        }
        public IHttpActionResult Getlocations(string query)
        {
            try
            {
                IBusinessEngine business =   _BusinessRepositoryFactory.GetBusinessEngine<IBusinessEngine>();
                return Content(HttpStatusCode.InternalServerError,  business.Getlocations(query));
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
                IBusinessEngine business = _BusinessRepositoryFactory.GetBusinessEngine<IBusinessEngine>();
                return Content(HttpStatusCode.InternalServerError, business.GetPictureDetails(Id));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}