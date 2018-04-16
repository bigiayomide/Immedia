using Immedia.Picture.Api.Entities;
using Immedia.Picture.Data.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immedia.Picture.Data.Repository
{
    [Export(typeof(IPlaceRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PlaceRepository : RepositoryService<Place>, IPlaceRepository
    {
        protected override Place AddEntity(ApplicationDbContext entityContext, Place entity)
        {
            return entityContext.Places.Add(entity);
        }

        protected override Place UpdateEntity(ApplicationDbContext entityContext, Place entity)
        {
            return (from e in entityContext.Places
                    where e.PlaceId == entity.PlaceId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Place> GetEntities(ApplicationDbContext entityContext)
        {
            return from e in entityContext.Places
                   select e;
        }

        protected override Place GetEntity(ApplicationDbContext entityContext, string id)
        {
            var query = (from e in entityContext.Places
                         where e.PlaceId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
        public void SavePlacePhoto(string placeId, List<Photo> photos)
        {
            using (ApplicationDbContext entityContext = new ApplicationDbContext())
            {
                Place place = GetEntity(entityContext, placeId);
                foreach (var item in photos)
                {

                    if (place.Photos.Where(x => x.Id == item.Id) == null)
                    {
                        entityContext.Photos.Attach(item);
                        place.Photos.Add(item);
                    }
                }
                entityContext.SaveChanges();
            }
        }
        public void SaveLocations(List<Place> places)
        {
            using (ApplicationDbContext entityContext = new ApplicationDbContext())
            {
                foreach (var item in places)
                {
                    Place place = GetEntity(entityContext,item.PlaceId);
                    if (place == null)
                        entityContext.SaveChanges();
                }
            }
        }
    }
}
