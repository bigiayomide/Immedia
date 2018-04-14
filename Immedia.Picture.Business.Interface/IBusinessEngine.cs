using GoogleMaps.LocationServices;
using Immedia.Picture.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immedia.Picture.Business.Interface
{
    public interface IBusinessEngine
    {
        Task<Result> GetLocationPictureLatLonAsync(string locationId, int? page, string userId);
        void SavePictureforUser(string Userid, Photo photo);
        MapPoint GetLocationLonLat(string address);
        Photo GetPictureDetails(int id);
        Task<List<Place>> Getlocations(string query);
    }
}