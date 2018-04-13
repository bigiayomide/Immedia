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
    [Export(typeof(IUserRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserRepository : RepositoryService<ApplicationUser>, IUserRepository
    {
        protected override ApplicationUser AddEntity(ApplicationDbContext entityContext, ApplicationUser entity)
        {
            return entityContext.Users.Add(entity);
        }

        protected override ApplicationUser UpdateEntity(ApplicationDbContext entityContext, ApplicationUser entity)
        {
            return (from e in entityContext.Users
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ApplicationUser> GetEntities(ApplicationDbContext entityContext)
        {
            return from e in entityContext.Users
                   select e;
        }

        protected override ApplicationUser GetEntity(ApplicationDbContext entityContext, string id)
        {
            var query = (from e in entityContext.Users
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public ApplicationUser GetByLogin(string email)
        {
            using (ApplicationDbContext entityContext = new ApplicationDbContext())
            {

                return (from a in entityContext.Users
                        where a.Email == email
                        select a).FirstOrDefault();
            }
        }

        public void SavePictureForUser(Photo photo, string id)
        {
            using (ApplicationDbContext entityContext = new ApplicationDbContext())
            {
                ApplicationUser user = GetEntity(entityContext, id);
                user.Photos.Add(photo);
                entityContext.SaveChanges();
            }
        }
        public void RemovePictureForUser(Photo photo, string id)
        {
            using (ApplicationDbContext entityContext = new ApplicationDbContext())
            {
                ApplicationUser user = GetEntity(entityContext, id);
                user.Photos.Remove(photo);
                entityContext.SaveChanges();
            }
        }

        public void SaveUserLocation(string id, Place place)
        {
            using (ApplicationDbContext entityContext = new ApplicationDbContext())
            {
                ApplicationUser user = GetEntity(entityContext, id);
                user.Places.Add(place);
                entityContext.SaveChanges();
            }
        }
    }
}
