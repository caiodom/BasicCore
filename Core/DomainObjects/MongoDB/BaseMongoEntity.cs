using Core.DomainObjects.MongoDB.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainObjects.MongoDB
{
    public class BaseMongoEntity: IBaseMongoEntity
    {
        [BsonId]
        public string Id { get; set; }
    }
}
