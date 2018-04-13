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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Result> GetLocationPictureLatLonAsync(Place place,int? page,string userId)
        {
            try
            {
                Result result = await _searchRequest.GetPhotosforLocationAsync(place.Latitude, place.Longitude, page.Value);

                _UserRepository.SaveUserLocation(userId, place);
                _PlaceRepository.SavePlacePhoto(place.PlaceId, result.Photos);
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

        public void SavePictureforUser(string userid, Photo photo)
        {
            try
            {
                if (!string.IsNullOrEmpty(userid) && photo != null)
                {
                    _UserRepository = _DataRepositoryFactory.GetDataRepository<IUserRepository>();
                    ApplicationUser User= _UserRepository.Get(userid);
                    if(User!=null)
                    {
                        if (User.Photos.Where(x=>x.Id==photo.Id)==null)
                            _UserRepository.SavePictureForUser(photo, userid);
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
            await Task.Factory.StartNew(() => {
                foreach (var item in places)
                {
                    Place place = _PlaceRepository.Get(item.PlaceId);
                    if (place == null)
                        _PlaceRepository.Add(item);
                }
            }, TaskCreationOptions.LongRunning);
            return places;
        }
        public void SaveUserLocation(string locationId, Place place)
        {
            _UserRepository.SaveUserLocation(locationId, place);
        }
            
        public ApiRequest Api;
        public string Apikey { get; } = "7e40b74b14e4b62ddd2cadb193d646ed";
        public void Init()
        {
            Api = new ApiRequest(Apikey);
        }
    }
}

