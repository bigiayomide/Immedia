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
    [Export(typeof(IPhotoRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PhotoRepository : RepositoryService<Photo>, IPhotoRepository
    {
        protected override Photo AddEntity(ApplicationDbContext entityContext, Photo entity)
        {
            return entityContext.Photos.Add(entity);
        }

        protected override Photo UpdateEntity(ApplicationDbContext entityContext, Photo entity)
        {
            return (from e in entityContext.Photos
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Photo> GetEntities(ApplicationDbContext entityContext)
        {
            return from e in entityContext.Photos
                   select e;
        }

        protected override Photo GetEntity(ApplicationDbContext entityContext, string id)
        {
            var query = (from e in entityContext.Photos
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
    }
}
