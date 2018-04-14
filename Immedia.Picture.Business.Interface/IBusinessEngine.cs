﻿using GoogleMaps.LocationServices;
using Immedia.Picture.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Immedia.Picture.Business.Interface
{
    public interface IBusinessEngine
    {
        Task<Result> GetLocationPicturesByIdAsync(Place place, int? page, string userId);
        void SavePictureforUser(Photo photo, string userId);
        MapPoint GetLocationLonLat(string address);
        Task<Result> GetLocationByLonLat(string longitude, string latitude, int? page);
        Photo GetPictureDetails(int id);
        Task<List<Place>> Getlocations(string query);
    }
}