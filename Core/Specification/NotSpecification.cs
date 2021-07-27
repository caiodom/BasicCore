using Core.Specification.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public sealed class NotSpecification<T> : Specification<T> where T : class
    {

        private Expression<Func<T, bool>> _OriginalCriteria;


        public NotSpecification(ISpecification<T> originalSpecification)
        {
            if (originalSpecification == (ISpecification<T>)null)
                throw new ArgumentNullException(nameof(originalSpecification));

            _OriginalCriteria = originalSpecification.IsSatisfiedBy();
        }

        public NotSpecification(Expression<Func<T, bool>> originalExpression)
        {
            if (originalExpression == (Expression<Func<T, bool>>)null)
                throw new ArgumentNullException(nameof(originalExpression));

            _OriginalCriteria = originalExpression;
        }


        public override Expression<Func<T, bool>> IsSatisfiedBy()
                            => Expression.Lambda<Func<T, bool>>(Expression.Not(_OriginalCriteria.Body),
                                                                                _OriginalCriteria.Parameters.Single());


    }
}
