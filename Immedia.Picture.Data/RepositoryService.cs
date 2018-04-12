using Immedia.Picture.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Immedia.Picture.Api.Core.Common;
using System.Threading.Tasks;

namespace Immedia.Picture.Data
{
    public abstract class RepositoryService<T> : RepositoryService<T, ApplicationDbContext>
       where T : class, new()
    { }
}
