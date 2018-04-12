using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immedia.Picture.Api.Core.Contracts
{

    public interface IRepositoryService<T> where T : class
    {
        T Add(T entity);

        void Remove(T entity);

        void Remove(string id);

        T Update(T entity);

        IEnumerable<T> Get();

        T Get(string id);

    }
}
