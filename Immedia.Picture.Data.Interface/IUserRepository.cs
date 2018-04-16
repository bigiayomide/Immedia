using Immedia.Picture.Api.Core.Contracts;
using Immedia.Picture.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immedia.Picture.Data.Interface
{
    public interface IUserRepository : IDataRepository<ApplicationUser>
    {
        ApplicationUser GetByLogin(string email);
        void SavePictureForUser(Photo photo, string id);
        void SaveUserLocation(string id, Place place);
        List<Place> GetUserLocations(string id);
        List<Photo> GetUserPhotos(string userId);
    }
}
