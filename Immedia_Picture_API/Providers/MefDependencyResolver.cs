using Immedia.Picture.Api.Core.Common.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;

namespace Immedia_Picture_API.Providers
{
    public class MefDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        public MefDependencyResolver(CompositionContainer container)
        {
            _Container = container;
        }

        CompositionContainer _Container;

        public object GetService(Type serviceType)
        {
            return _Container.GetExportedValueByType(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _Container.GetExportedValuesByType(serviceType);
        }
    }
}