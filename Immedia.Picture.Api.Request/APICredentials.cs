using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immedia.Picture.Api.Request
{
    public class ApiCredentials
    {
        public ApiRequest Api;
        public string Apikey { get; } = "015f6549349d226bd35da23bef97436f";
        public void Init()
        {
            Api = new ApiRequest( Apikey);
        }
    }
}
