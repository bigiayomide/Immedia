using Immedia.Picture.Api.Core.Contracts;
using Immedia.Picture.Api.Entities;
using Immedia.Picture.Api.Request;
using Immedia.Picture.Api.Request.Requests;
using Immedia.Picture.Data;
using Immedia.Picture.Data.Interface;
using Immedia.Picture.Data.Repository;
using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace Immedia_Picture_API.Controllers
{
    [RoutePrefix("api/Picture")]
    public class PictureController : PictureApiController
    {
        ISearchRequest _searchRequest;
        public PictureController()
        {
            Init();
            _searchRequest = Api.SearchRequest;
        }


        [Route("GetLocationPictures")]
        public async  Task<IHttpActionResult> GetLocationPicturesAsync(string lon, string lat, int? page)
        {

            try
            {
                Result result = await _searchRequest.GetPhotosforLocationAsync(lat, lon, page.Value);
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


        [Import]
        IDataRepositoryFactory _IDataRepositoryFactory;
        IUserRepository _IUserRepository;
        public IHttpActionResult SavePictureforUser(string Userid, Photo photo)
        {
            try
            {
                if (!string.IsNullOrEmpty(Userid) && photo != null)
                {
                    _IUserRepository = _IDataRepositoryFactory.GetDataRepository<IUserRepository>();
                    _IUserRepository.SavePictureForUser(photo, Userid);
                    return Ok();
                }
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}