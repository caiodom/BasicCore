using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Interface
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
