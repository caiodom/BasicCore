using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync(params Action[] actionValidations);
        bool Commit(params Action[] actionValidations);
    }
}
