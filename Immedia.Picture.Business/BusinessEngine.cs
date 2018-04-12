using Immedia.Picture.Api.Core.Contracts;
using Immedia.Picture.Business.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immedia.Picture.Business
{
    [Export(typeof(IBusinessEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BusinessEngine: IBusinessEngine
    {
        [ImportingConstructor]
        public BusinessEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        IDataRepositoryFactory _DataRepositoryFactory;
    }
}

