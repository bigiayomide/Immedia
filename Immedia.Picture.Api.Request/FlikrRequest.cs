using Immedia.Picture.Api.Request;
using Immedia.Picture.Api.Request.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immedia.Picture.Api.Request
{
    public class FlikrRequest: ApiCredentials
    {
        private SearchRequest _searchRequest;
        public FlikrRequest()
        {
            Init();
            _searchRequest = Api.SearchRequest;
        }
    }
}
