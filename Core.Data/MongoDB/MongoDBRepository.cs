using Core.Data.MongoDB.Interface;
using Core.DomainObjects.MongoDB.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.MongoDB
{
    public class MongoDBRepository<T>: IMongoDBRepository<T> where T:IBaseMongoEntity
    {
        protected readonly IMongoCollection<T> _mongoCollection;

        public MongoDBRepository(IDataBaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database=client.GetDatabase(settings.DatabaseName);

            _mongoCollection=database.GetCollection<T>(settings.CollectionName);
        }

        public T Create(T entity)
        {
            entity.Id = Guid.NewGuid()
                            .ToString();

            _mongoCollection.InsertOne(entity);
            return entity;

        }

        public List<T> Get()
        {
            return _mongoCollection
                    .Find(entity => true)
                    .ToList();
        }

        public T Get(string id)
        {
            return _mongoCollection
                     .Find<T>(e => e.Id == id)
                     .FirstOrDefault();
        }

        public void Remove(T entity)
        {
            _mongoCollection.DeleteOne(e=>e.Id== entity.Id);
        }

        public void Remove(string id)
        {
            _mongoCollection.DeleteOne(e => e.Id == id);
        }

        public void Update(string id, T entity)
        {
            _mongoCollection.ReplaceOne(e => e.Id == id, entity);
        }
    }
}
