using System.Collections.Generic;

namespace Immedia.Picture.Api.Request.Requests
{
    public class Photo
    {
        public string id { get; set; }
        public string title { get; set; }
    }

    public class Photos
    {
        public int page { get; set; }
        public int pages { get; set; }
        public int perpage { get; set; }
        public int total  { get; set; }
        public List<Photo> photo { get; set; }
    }
}