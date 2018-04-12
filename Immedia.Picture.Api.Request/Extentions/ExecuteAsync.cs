using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Immedia_Picture_API.Extentions
{
    public class Extentions
    {
        public Task<T> ExecuteAsync<T>(RestRequest request,RestClient client) where T : new()
        {
            var taskCompletionSource = new TaskCompletionSource<T>();
            client.ExecuteAsync<T>(request, (response) => taskCompletionSource.SetResult(response.Data));
            return taskCompletionSource.Task;
        }
    }
}