using Immedia.Picture.Api.Request;
using Immedia.Picture.Api.Request.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Immedia.Picture.Api.Controllers
{
    public class HomeController : Controller
    {
        FlikrRequest _flikrRequest;
        public HomeController()
        {
            _flikrRequest = new FlikrRequest();
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Title = "Home Page";
            var url = _flikrRequest..GetPhotos("-29.8586800", "31.0218400");

            return View();
        }
    }
}
