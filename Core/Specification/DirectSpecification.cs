using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public sealed class DirectSpecification<T> : Specification<T> where T : class
    {
        private readonly Expression<Func<T, bool>> _matchingCriteria;

        public DirectSpecification(Expression<Func<T, bool>> matchingCriteria)
        {
            if (matchingCriteria == (Expression<Func<T, bool>>)null)
                throw new ArgumentNullException("matchingCriteria");

            _matchingCriteria = matchingCriteria;
        }

        public override Expression<Func<T, bool>> IsSatisfiedBy()
                                        => _matchingCriteria;
    }
}
