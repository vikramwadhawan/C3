using System;
using System.Linq;
using System.Linq.Expressions;

namespace Colligo.O365Product.RM.Admin.Framework.Data
{
    public class OrderByClause<T, TProperty> : IOrderByClause<T> where T : class
    {
        private OrderByClause()
        {

        }
        /// <summary>
        /// Pass constructor to apply sorting
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="sortDirection"></param>
        public OrderByClause(Expression<Func<T, TProperty>> orderBy, SortDirection sortDirection = SortDirection.Ascending)
        {
            OrderBy = orderBy;
            SortDirection = sortDirection;
        }

        /// <summary>
        /// Order by expression
        /// </summary>
        private Expression<Func<T, TProperty>> OrderBy { get; set; }

        /// <summary>
        /// Sort direction
        /// </summary>
        private SortDirection SortDirection { get; set; }

        /// <summary>
        /// Applysorting logic
        /// </summary>
        /// <param name="query"></param>
        /// <param name="firstSort"></param>
        /// <returns></returns>
        public IOrderedQueryable<T> ApplySort(IQueryable<T> query, bool firstSort)
        {
            if (SortDirection == SortDirection.Ascending)
            {
                if (firstSort)
                {
                    return query.OrderBy(OrderBy);
                }
                else
                {
                    return ((IOrderedQueryable<T>)query).ThenBy(OrderBy);
                }
            }
            else
            {
                if (firstSort)
                {
                    return query.OrderByDescending(OrderBy);
                }
                else
                {
                    return ((IOrderedQueryable<T>)query).ThenByDescending(OrderBy);
                }
            }

        }

    }
}
