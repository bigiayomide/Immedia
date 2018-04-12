﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Immedia.Picture.Api.Request.Requests
{
    public class Photo
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "owner")]
        public string Owner { get; set; }
        [DataMember(Name = "secret")]
        public string Secret { get; set; }
        [DataMember(Name = "server")]
        public string Server { get; set; }
        [DataMember(Name = "farm")]
        public string  Farm { get; set; }
        public string ImgUrl
        {
            get
            {
                return string.Format("http://farm{0}.static.flickr.com/{1}/{2}_{3}.jpg", Farm, Server, Id, Secret);
            }
        }
    }

    public class Result
    {
        [DataMember(Name = "page")]
        public int Page { get; set; }
        [DataMember(Name = "pages")]
        public int Pages { get; set; }
        [DataMember(Name = "perpage")]
        public int Perpage { get; set; }
        [DataMember(Name = "total")]
        public int Total  { get; set; }
        [DataMember(Name = "photos")]
        public List<Photo> Photos { get; set; }

    }
}