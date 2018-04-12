using System;
using System.Threading.Tasks;
using Immedia.Picture.Api.Entities;
using Immedia.Picture.Api.Request.Requests;
using Immedia.Picture.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Immedia.Picture.Picture.Request.Test.Tests
{
    [TestClass]
    public class QuoteTests : TestBase
    {
        private ISearchRequest _searchRequest;

        [TestInitialize]
        public void LocalInit()
        {
            _searchRequest = Api.SearchRequest; // Api is declared on TestBase
        }
        [TestMethod]
        public async Task Test_PhotosforLocationAsync_NotNull()
        {
            string lat = "29.8587"; string lon = "31.0218"; int page = 1;

            Result result = await _searchRequest.GetPhotosforLocationAsync(lat, lon, page);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task Test_PhotosforLocationAsync_Paging()
        {
            string lat = "40.7128"; string lon = "74.0060"; int page = 1;
            Result result2 = new Result();

            Result result = await _searchRequest.GetPhotosforLocationAsync(lat, lon, page);

            while (result.Page <= result.Pages)
            {
                result2 = await _searchRequest.GetPhotosforLocationAsync(lat, lon, result.Page);
                result.Page++;
            }

            Assert.AreNotEqual(result, result2);
        }
        [TestMethod]
        public async Task Test_GetPhotoDetailsAsync_NotNull()
        {
            int id = 2636;

            Photo result = await _searchRequest.GetPhotoDetailsAsync(id);

            Assert.IsNotNull(result);
        }
    }
}
