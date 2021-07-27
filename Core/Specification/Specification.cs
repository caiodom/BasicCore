using Core.Specification.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public abstract class Specification<T> : ISpecification<T> where T : class
    {
        #region >> ISpecification<T> Members <<
        public abstract Expression<Func<T, bool>> IsSatisfiedBy();

        #endregion

        public static Specification<T> operator &(Specification<T> leftSideSpecification, Specification<T> rightSideSpecification)
                            => new AndSpecification<T>(leftSideSpecification, rightSideSpecification);

        public static Specification<T> operator |(Specification<T> leftSideSpecification, Specification<T> rightSideSpecification)
                                       => new OrSpecification<T>(leftSideSpecification, rightSideSpecification);

        public static Specification<T> operator !(Specification<T> specification)
                                            => new NotSpecification<T>(specification);

        public static bool operator false(Specification<T> specification)
        {
            return false;
        }


        public static bool operator true(Specification<T> specification)
        {
            return false;
        }


    }
}
