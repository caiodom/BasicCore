using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public abstract class RuleSpecification<T> where T : class
    {
        public abstract bool IsSatisfiedBy(T entity, out string message);

    }
}
