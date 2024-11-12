using System.Linq;
using System.Linq.Expressions;
using WebConfigurationControl.Common.Models.Ordering;

namespace WebConfigurationControl.Common.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> Order<T>(this IQueryable<T> query, IEnumerable<GlobalEntityOrdering> ordering)
        {
            var firstIteration = true;
            foreach (var order in ordering.OrderBy(e=> e.Order))
            {
                if(order.Direction == Enums.FilterDirection.Asc)
                {
                    query = query.OrderBy(order, firstIteration);
                } else
                {
                    query = query.OrderByDescending(order, firstIteration);
                }

                firstIteration = false;
            }

            return query;
        }

        private static IQueryable<T> OrderBy<T>(this IQueryable<T> query, GlobalEntityOrdering order, bool firstIteration = false)
        {
            var keySelector = GetKeySelectorExpression<T>(order.FieldName);

            if(firstIteration)
            {
                query = query.OrderBy(keySelector);
            } else
            {
                var orderedQuery = ((IOrderedQueryable<T>)query).ThenBy(keySelector);

                return orderedQuery;
            }

            return query;
        }
        private static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, GlobalEntityOrdering order, bool firstIteration = false)
        {
            var keySelector = GetKeySelectorExpression<T>(order.FieldName);

            if (firstIteration)
            {
                query = query.OrderByDescending(keySelector);
            }
            else
            {
                var orderedQuery = ((IOrderedQueryable<T>)query).ThenByDescending(keySelector);

                return orderedQuery;
            }

            return query;
        }

        private static Expression<Func<T, object>> GetKeySelectorExpression<T>(string fieldName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, fieldName);
            var convert = Expression.Convert(property, typeof(object));
            var keySelector = Expression.Lambda<Func<T, object>>(convert, parameter);

            return keySelector;
        }
    }
}
