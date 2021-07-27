using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBaseAppService<TSrc, TDest>
    {

        Task<IEnumerable<TSrc>> GetAsync(bool asNoTracking = true);

        Task<TSrc> GetByIdAsync(Guid entityId, bool asNoTracking = true);

        Task AddAsync(TSrc entity);


        Task AddCollectionAsync(IEnumerable<TSrc> entities);


        Task UpdateAsync(TSrc entity);


        Task UpdateCollectionAsync(IEnumerable<TSrc> entities);

        Task RemoveAsync(TSrc entity);





    }
}
