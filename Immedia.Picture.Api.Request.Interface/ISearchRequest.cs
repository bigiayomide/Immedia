using Immedia.Picture.Api.Entities;
using System.Threading.Tasks;

namespace Immedia.Picture.Data
{
    public interface ISearchRequest
    {
        /// <summary>
        /// Get's photos based on user location  from Flikr Api
        /// </summary>
        /// <param name="lat">Latitude of the Logation</param>
        /// <param name="lon">Logitute of the location</param>
        /// <param name="page"></param>
        /// <returns>List of Photos</returns>
        Task<Result> GetPhotosforLocationAsync(string lat, string lon, int page = 1);

        /// <summary>
        /// Get's Details of an Image
        /// </summary>
        /// <param name="id">Id of the photo</param>
        /// <returns>A Photo</returns>
        Task<Photo> GetPhotoDetailsAsync(int id);
    }
}
