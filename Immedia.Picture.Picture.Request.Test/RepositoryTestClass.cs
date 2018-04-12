using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Immedia.Picture.Api.Core.Common.Core;
using Immedia.Picture.Api.Entities;
using Immedia.Picture.Data.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Immedia.Picture.Picture.Request.Test
{
    public class RepositoryTestClass
    {
        public RepositoryTestClass()
        {
            ObjectBase.Container.SatisfyImportsOnce(this);
        }

        public RepositoryTestClass(IUserRepository IUserRepository)
        {
            _IUserRepository = IUserRepository;
        }

        [Import]
        IUserRepository _IUserRepository;

        public IEnumerable<ApplicationUser> GetUsers()
        {
            IEnumerable<ApplicationUser> users = _IUserRepository.Get();

            return users;
        }
    }
}
