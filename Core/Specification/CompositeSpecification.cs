using Core.Specification.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public abstract class CompositeSpecification<T> : Specification<T> where T : class
    {
        
        public abstract ISpecification<T> LeftSideSpecification { get; }

       
        public abstract ISpecification<T> RightSideSpecification { get; }
    }
}
