using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification.Util
{

    public sealed class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;


        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this._map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }


        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> externMap, Expression expToReplace)
                    => new ParameterRebinder(externMap).Visit(expToReplace);



        protected override Expression VisitParameter(ParameterExpression parameterToReplace)
        {
            if (_map.TryGetValue(parameterToReplace, out ParameterExpression replacement))
                parameterToReplace = replacement;

            return base.VisitParameter(parameterToReplace);
        }

    }
}
