using System;
using Immedia.Picture.Api.Request;
using Immedia.Picture.Api.Request.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Immedia.Picture.Picture.Request.Test
{
    [TestClass]
    public class TestBase
    {
        public ApiRequest Api;
        public string Apikey { get; } = "7e40b74b14e4b62ddd2cadb193d646ed";

        [TestInitialize]
        public void Init()
        {
            Api = new ApiRequest(Apikey);
        }
    }
}
