using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Hangfire;
using HangFire.MEF;
using Immedia.Picture.Api.Core.Common.Core;
using System.Reflection;
using System.IO;
using System.Web.Http;

[assembly: OwinStartup(typeof(Immedia.Picture.Api.Startup))]

namespace Immedia.Picture.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            System.Web.Http.GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            
            #region TODO:// Configure HangFire with MEF
            //Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            //app.UseHangfireDashboard();
            //app.UseHangfireServer();
            #endregion
        }
    }
}
