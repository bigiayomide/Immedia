using GoogleMaps.LocationServices;
using Immedia.Picture.Api.Core.Contracts;
using Immedia.Picture.Api.Entities;
using Immedia.Picture.Api.Request;
using Immedia.Picture.Business.Interface;
using Immedia.Picture.Data;
using Immedia.Picture.Data.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Immedia.Picture.Api.Core.Common.Core;
using Immedia.Picture.Data.Repository;
using System.Web.Hosting;

namespace Immedia.Picture.Business
{
    [Export(typeof(IBusinessEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BusinessEngine:  IBusinessEngine
    {
        GoogleLocationService _LocationService;
        ISearchRequest _searchRequest;
        [ImportingConstructor]
        public BusinessEngine(IDataRepositoryFactory DataRepositoryFactory, 
            IUserRepository UserRepository, IPlaceRepository PlaceRepository, IPhotoRepository PhotoRepository)
        {
            Init();
            _searchRequest = Api.SearchRequest;
            _DataRepositoryFactory = DataRepositoryFactory;
            _UserRepository=  UserRepository;
            _PlaceRepository = PlaceRepository;
            _PhotoRepository = PhotoRepository;
        }

        IDataRepositoryFactory _DataRepositoryFactory;
        IUserRepository _UserRepository;
        IPlaceRepository _PlaceRepository;
        IPhotoRepository _PhotoRepository;

        public MapPoint GetLocationLonLat(string address)
        {
            _LocationService = new GoogleLocationService();
            var point = _LocationService.GetLatLongFromAddress(address);

            return point;
        }
        /// <summary>
        /// Get's and saves Pictures by Id and also saves the Location of the User 
        /// </summary>
        /// <param name="place">The Location that was searched by the user </param>
        /// <param name="page">Use this for paging</param>
        /// <param name="userId">Id of the User</param>
        /// <returns></returns>
        public async Task<Result> GetLocationPicturesByIdAsync(Place place, int? page, string userId)
        {
            SaveUserLocation(place, userId);
            Result result = await _searchRequest.GetPhotosforLocationAsync(place.Latitude, place.Longitude, page.Value);

            #region   TODO:// Implement Mef with HangFire
            //BackgroundJob.Enqueue(() => SavePictures(result, place));
            #endregion

            HostingEnvironment.QueueBackgroundWorkItem(ct => SavePictures(result, place));

            return result;

        }
        /// <summary>
        /// Saves Pictures for a particular Place
        /// </summary>
        /// <param name="result"></param>
        /// <param name="place">The place object that needs to be saved</param>
        /// <returns></returns>
        public async Task SavePictures(Result result, Place place)
        {

            while (result.Pages != result.Page)
            {
                _PlaceRepository.SavePlacePhoto(place.PlaceId, result.Photos);
                result.Page++;
                result = await _searchRequest.GetPhotosforLocationAsync(place.Latitude, place.Longitude, result.Page);
            }
        }
        /// <summary>
        /// Get's Picture Location by Longitiude and Latitude and saves the pictures to the Database
        /// </summary>
        /// <param name="longitude">Location Longitude</param>
        /// <param name="latitude">Location Latitude</param>
        /// <param name="page">Use this for Paging</param>
        /// <returns></returns>
        public async Task<Result> GetLocationByLonLat(string longitude, string latitude, int? page)
        {
            Result result = await _searchRequest.GetPhotosforLocationAsync(latitude, longitude, page.Value);
            Place place = await _searchRequest.GetLocationByLonLat(latitude, longitude);

            HostingEnvironment.QueueBackgroundWorkItem(ct => SavePictures(result, place));

            #region   TODO:// Implement Mef with HangFire
            //BackgroundJob.Enqueue(() => SavePictures(result, place));
            #endregion


            return result;
        }
        /// <summary>
        /// Saves Picture for a User 
        /// </summary>
        /// <param name="photo">Picture that is needed to be saved</param>
        /// <param name="userId">Id of the User</param>
        public void SavePictureforUser(Photo photo, string userId)
        {
            if (!string.IsNullOrEmpty(userId) && photo != null)
            {
                _UserRepository = _DataRepositoryFactory.GetDataRepository<IUserRepository>();
                _UserRepository.SavePictureForUser(photo, userId);
            }
        }
        /// <summary>
        /// Get Picture Details
        /// </summary>
        /// <param name="id">Id of the Picture</param>
        /// <returns>Photo Object</returns>
        public Photo GetPictureDetails(int id)
        {
            return _PhotoRepository.Get(id.ToString());
        }

        /// <summary>
        /// Search and Save Location if Location doesn't exist 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<Place>> Getlocations(string query)
        {

            List<Place> places = await _searchRequest.GetLocationquery(query);
            HostingEnvironment.QueueBackgroundWorkItem(ct => _PlaceRepository.SaveLocations(places));

            #region   TODO:// Implement Mef with HangFire
            //BackgroundJob.Enqueue(() => _PlaceRepository.SaveLocations(places));
            #endregion

            return places;
        }

        /// <summary>
        /// Save User Location 
        /// </summary>
        /// <param name="place">the Location of the User</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Place Object</returns>
        public Place SaveUserLocation(Place place, string userId)
        {

            if (_PlaceRepository.Get(place.PlaceId) == null)
            {
                _PlaceRepository.Add(place);
            }
            if (!String.IsNullOrEmpty(userId))
            {
                _UserRepository.SaveUserLocation(userId, place);
            }
            return place;

        }
        /// <summary>
        /// Get Saved User Photos
        /// </summary>
        /// <param name="userId">The Id of the User</param>
        /// <returns></returns>
        public Result GetUserPhotos(string userId)
        {
            return _UserRepository.GetUserPhotos(userId);
        }
        /// <summary>
        /// Get Saved User Location
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>List of Place</returns>
        public List<Place> GetUserLocations(string UserId)
        {
            return _UserRepository.GetUserLocations(UserId);

        }


        public ApiRequest Api;
        public string Apikey { get; } = "7e40b74b14e4b62ddd2cadb193d646ed";
        public void Init()
        {
            Api = new ApiRequest(Apikey);
        }
    }
}

