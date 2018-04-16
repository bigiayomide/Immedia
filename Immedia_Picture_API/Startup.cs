using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Hangfire;
using HangFire.MEF;
using Immedia.Picture.Api.Core.Common.Core;

[assembly: OwinStartup(typeof(Immedia.Picture.Api.Startup))]

namespace Immedia.Picture.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
