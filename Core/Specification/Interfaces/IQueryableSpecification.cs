using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification.Interfaces
{
    public interface IQueryableSpecification<T> where T : class
    {
        IEnumerable<T> IsSatisfiedBy(IQueryable<T> queryable);
    }
}
