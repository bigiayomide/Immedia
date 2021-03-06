﻿using Hangfire;
using HangFire.MEF;
using Immedia.Picture.Api.Bootstraper;
using Immedia.Picture.Api.Core.Common.Core;
using Immedia_Picture_API.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Immedia.Picture.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            System.Web.Http.GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            System.Web.Http.GlobalConfiguration.Configuration.Formatters
                .Remove(System.Web.Http.GlobalConfiguration.Configuration.Formatters.XmlFormatter);


            //Get Instances of Classes at Runtime and safe it in a container 
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            CompositionContainer container = MefLoader.Init(catalog.Catalogs);
            ObjectBase.Container = container;
            Hangfire.GlobalConfiguration.Configuration.UseMEFActivator(container);

            DependencyResolver.SetResolver(new MefDependencyResolver(container)); // view controllers
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new MefAPIDependencyResolver(container);
        }
    }
}
