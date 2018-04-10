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

        public string Username { get; } = "leonjvre@gmail.com";
        public string Password { get; } = "Tatss101!";

        public string Apikey { get; } = "{5AD9D7AA-4C22-4E92-9E93-0E9490CF7980}";

        public int CompanyId { get; } = 284796;

        public void Init()
        {
            Api = new ApiRequest(Username, Password, Apikey, CompanyId);
        }
    }
}
