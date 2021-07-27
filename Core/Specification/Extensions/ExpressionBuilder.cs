﻿using Core.Specification.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification.Extensions
{
    public static class ExpressionBuilder
    {
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters
                            .Select((f, i) => new { f, s = second.Parameters[i] })
                            .ToDictionary(p => p.s, p => p.f);

            var secondyBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            return Expression.Lambda<T>(merge(first.Body, secondyBody), first.Parameters);
        }


        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
                           => first.Compose(second, Expression.And);

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
                             => first.Compose(second, Expression.Or);



    }
}
