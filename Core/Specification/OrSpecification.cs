using Core.Specification.Extensions;
using Core.Specification.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public sealed class OrSpecification<T> : CompositeSpecification<T> where T : class
    {
        private readonly ISpecification<T> _RightSideSpecification = null;
        private readonly ISpecification<T> _LeftSideSpecification = null;

        public OrSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
        {
            if (leftSide == (ISpecification<T>)null)
                throw new ArgumentNullException("leftSide");

            if (rightSide == (ISpecification<T>)null)
                throw new ArgumentNullException("rightSide");

            this._LeftSideSpecification = leftSide;
            this._RightSideSpecification = rightSide;
        }

        public override ISpecification<T> RightSideSpecification { get { return _RightSideSpecification; } }
        public override ISpecification<T> LeftSideSpecification { get { return _LeftSideSpecification; } }

        public override Expression<Func<T, bool>> IsSatisfiedBy()
        {
            Expression<Func<T, bool>> left = _LeftSideSpecification.IsSatisfiedBy();
            Expression<Func<T, bool>> right = _RightSideSpecification.IsSatisfiedBy();

            return (left.Or(right));
        }
    }
}
