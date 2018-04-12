using Immedia.Picture.Api.Core.Common.Core;
using Immedia.Picture.Api.Core.Contracts;
using Immedia.Picture.Api.Entities;
using Immedia.Picture.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immedia.Picture.Picture.Request.Test
{
    public class RepositoryFactoryTestClass
    {
        public RepositoryFactoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryFactoryTestClass(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _DataRepositoryFactory;

        public IEnumerable<ApplicationUser> GetUsers()
        {
            IUserRepository carRepository = _DataRepositoryFactory.GetDataRepository<IUserRepository>();

            IEnumerable<ApplicationUser> users = carRepository.Get();

            return users;
        }
    }
}
