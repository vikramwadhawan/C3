using System.Linq;

namespace Colligo.O365Product.RM.Admin.Framework.Data
{
    public interface IOrderByClause<T>    where T : class
    {
        IOrderedQueryable<T> ApplySort(IQueryable<T> query, bool firstSort);
    }
}
