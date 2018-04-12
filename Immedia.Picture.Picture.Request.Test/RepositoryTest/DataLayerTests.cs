using Immedia.Picture.Api.Core.Contracts;
using Immedia.Picture.Api.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Text;
using System.Threading.Tasks;
using Immedia.Picture.Api.Core.Common.Core;
using Immedia.Picture.Api.Bootstraper;
using Immedia.Picture.Data.Interface;

namespace Immedia.Picture.Picture.Request.Test.RepositoryTest
{
    [TestClass]
    public class DataLayerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ObjectBase.Container = MefLoader.Init();
        }

        [TestMethod]
        public void test_repository_usage()
        {
            RepositoryTestClass repositoryTest = new RepositoryTestClass();

            IEnumerable<ApplicationUser> users = repositoryTest.GetUsers();

            Assert.IsTrue(users != null);
        }

        [TestMethod]
        public void test_repository_factory_usage()
        {
            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass();

            IEnumerable<ApplicationUser> clients = factoryTest.GetUsers();

            Assert.IsTrue(clients != null);
        }

        [TestMethod]
        public void test_repository_mocking()
        {
            List<ApplicationUser> clients = new List<ApplicationUser>()
            {
                new ApplicationUser() {  UserName = "ayomide@yahoo.com" },
                new ApplicationUser() { UserName = "immedia@yahoo.com" }
            };

            Mock<IUserRepository> mockCarRepository = new Mock<IUserRepository>();
            mockCarRepository.Setup(obj => obj.Get()).Returns(clients);

            RepositoryTestClass repositoryTest = new RepositoryTestClass(mockCarRepository.Object);

            IEnumerable<ApplicationUser> ret = repositoryTest.GetUsers();

            Assert.IsTrue(ret == clients);
        }

        [TestMethod]
        public void test_factory_mocking1()
        {
            List<ApplicationUser> clients = new List<ApplicationUser>()
            {
                new ApplicationUser() {  UserName = "ayomide@yahoo.com" },
                new ApplicationUser() { UserName = "immedia@yahoo.com" }
            };

            Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.GetDataRepository<IUserRepository>().Get()).Returns(clients);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<ApplicationUser> ret = factoryTest.GetUsers();

            Assert.IsTrue(ret == clients);
        }

        [TestMethod]
        public void test_factory_mocking2()
        {
            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser() {  UserName = "ayomide@yahoo.com" },
                new ApplicationUser() { UserName = "immedia@yahoo.com" }
            };

            Mock<IUserRepository> mockCarRepository = new Mock<IUserRepository>();
            mockCarRepository.Setup(obj => obj.Get()).Returns(users);

            Mock<IDataRepositoryFactory> mockDataRepository = new Mock<IDataRepositoryFactory>();
            mockDataRepository.Setup(obj => obj.GetDataRepository<IUserRepository>()).Returns(mockCarRepository.Object);

            RepositoryFactoryTestClass factoryTest = new RepositoryFactoryTestClass(mockDataRepository.Object);

            IEnumerable<ApplicationUser> ret = factoryTest.GetUsers();

            Assert.IsTrue(ret == users);
        }
    }
}
