using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.BffServices.Interfaces
{
    public interface IBaseBFFService<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(Guid id);
        Task<T> PostAsync(T entity);
        Task<T> PutAsync(Guid id, T entity);
        Task<string> DeleteAsync(Guid id);

    }
}
