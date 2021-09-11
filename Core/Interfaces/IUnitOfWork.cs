using Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork<T>: IDisposable where T : BaseEntity,new()
    { 


        IRepository<T> BaseRepo { get; }
         Task<bool> CommitAsync(params Action[] actionValidations);
        bool Commit(params Action[] actionValidations);
    }
}
