using Immedia.Picture.Api.Core.Common.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Immedia_Picture_API.Providers
{
    public class MefAPIDependencyResolver : IDependencyResolver
    {
        public MefAPIDependencyResolver(CompositionContainer container)
        {
            _Container = container;
        }

        CompositionContainer _Container;

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            return _Container.GetExportedValueByType(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _Container.GetExportedValuesByType(serviceType);
        }

        public void Dispose()
        {
        }
    }
}