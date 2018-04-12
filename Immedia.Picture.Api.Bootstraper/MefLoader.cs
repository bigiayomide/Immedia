using Immedia.Picture.Api.Core.Contracts;
using Immedia.Picture.Business;
using Immedia.Picture.Data.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immedia.Picture.Api.Bootstraper
{
    public class MefLoader
    {
        public static CompositionContainer Init()
        {
            return Init(null);
        }
        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts)
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(BusinessEngine).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(UserRepository).Assembly));

            if (catalogParts != null)
                foreach (var part in catalogParts)
                    catalog.Catalogs.Add(part);

            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }
    }
}
