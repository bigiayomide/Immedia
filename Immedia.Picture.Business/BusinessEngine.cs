using GoogleMaps.LocationServices;
using Immedia.Picture.Api.Core.Contracts;
using Immedia.Picture.Api.Entities;
using Immedia.Picture.Api.Request;
using Immedia.Picture.Business.Interface;
using Immedia.Picture.Data;
using Immedia.Picture.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;

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

        public async Task<Result> GetLocationPicturesByIdAsync(Place place, int? page,string userId)
        {
            //try
            //{
                SaveUserLocation(place,  userId);
                Result result = await _searchRequest.GetPhotosforLocationAsync(place.Latitude, place.Longitude, page.Value);

            BackgroundJob.Enqueue(() =>  SavePictures(result, place));

            if (result != null)
                {
                    return result;
                }

                return null;
            //}
            //catch (Exception ex)
            //{
            //    return null;

            //}
        }
        public async Task SavePictures(Result result,Place place)
        {
            while (result.Pages != result.Page)
            {
                _PlaceRepository.SavePlacePhoto(place.PlaceId, result.Photos);
                result.Page++;
                result = await _searchRequest.GetPhotosforLocationAsync(place.Latitude, place.Longitude, result.Page);
            }
            int i = 0;
        }
        public async Task<Result> GetLocationByLonLat(string longitude, string latitude, int? page)
        {
            try
            {
                Result result = await _searchRequest.GetPhotosforLocationAsync(latitude,longitude,page.Value);
                Place place = await _searchRequest.GetLocationByLonLat(latitude, longitude);
                BackgroundJob.Enqueue(() => SavePictures(result, place));
                if (result != null)
                {
                    return result;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public void SavePictureforUser(Photo photo,string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId) && photo != null)
                {
                    _UserRepository = _DataRepositoryFactory.GetDataRepository<IUserRepository>();
                    ApplicationUser User= _UserRepository.Get(userId);
                    if(User!=null)
                    {
                        if (User.Photos.Where(x=>x.Id==photo.Id)==null)
                            _UserRepository.SavePictureForUser(photo, userId);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public Photo GetPictureDetails(int id)
        {
            return _PhotoRepository.Get(id.ToString());
        }

        /// <summary>
        /// Search and Save Location if Location doesn't exist 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async  Task<List<Place>> Getlocations(string query)
        {
            List<Place> places= await _searchRequest.GetLocationquery(query);
            BackgroundJob.Enqueue(() => _PlaceRepository.SaveLocations(places) );
            return places;
        }
        public Place SaveUserLocation(Place place, string userId)
        {
            
            if(_PlaceRepository.Get(place.PlaceId)==null)
            {
                _PlaceRepository.Add(place);
            }
            if (!String.IsNullOrEmpty(userId))
            {
                _UserRepository.SaveUserLocation(place.PlaceId, place);
            }
            return place;
        }
            
        public ApiRequest Api;
        public string Apikey { get; } = "7e40b74b14e4b62ddd2cadb193d646ed";
        public void Init()
        {
            Api = new ApiRequest(Apikey);
        }
    }
}

