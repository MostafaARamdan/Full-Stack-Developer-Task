using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Persistence.Helpers
{
    public static class SortingHelper
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            var parameter = Expression.Parameter(source.ElementType, string.Empty);
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = "OrderBy";
            var methodCallExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { source.ElementType, property.Type },
                source.Expression,
                Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            var parameter = Expression.Parameter(source.ElementType, string.Empty);
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = "OrderByDescending";
            var methodCallExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { source.ElementType, property.Type },
                source.Expression,
                Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}
