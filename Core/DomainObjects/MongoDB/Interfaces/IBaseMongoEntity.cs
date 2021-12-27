using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainObjects.MongoDB.Interfaces
{
    public interface IBaseMongoEntity
    {
        string Id { get; set; }
    }
}
