using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.MongoDB.Interface
{
    public interface IMongoDBRepository<T>
    {
        List<T> Get();
        T Get(string id);
        T Create(T entity);
        void Update(string id,T entity);
        void Remove(T entity);
        void Remove(string id);


    }
}
