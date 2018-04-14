using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Immedia.Picture.Api.Entities
{
    public class Place
    {
        [Key]
        [DataMember(Name = "place_id")]
        public string PlaceId { get; set; }
        [DataMember(Name = "woe_id")]
        public string WoeId { get; set; }
        [DataMember(Name = "latitude")]
        public string Latitude { get; set; }
        [DataMember(Name = "longitude")]
        public string Longitude { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "Value")]
        public string Value { get; set; }

        [DataMember(Name= "photos")]
        public virtual List<Photo> Photos { get; set; }

        public virtual List<ApplicationUser> Users { get; set; }
    }

    public class PlaceEntity
    {
        public string PlaceId { get; set; }
        public string WoeId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
