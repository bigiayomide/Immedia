using Immedia.Picture.Api.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Immedia.Picture.Api.Entities;

namespace Immedia.Picture.Data.Interface
{
    public interface IPlaceRepository : IDataRepository<Place>
    {
        void SavePlacePhoto(string placeId, List<Photo> photos);
    }
}
