using Newtonsoft.Json;
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
        public Place()
        {
            Photos = new List<Photo>();
            Users = new List<ApplicationUser>();
        }
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
        [JsonIgnore]
        [XmlIgnore]
        public virtual List<Photo> Photos { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public virtual List<ApplicationUser> Users { get; set; }
    }
}
