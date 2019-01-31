using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Admin.Framework.Data
{
    public interface IRepository<T> : IDisposable where T : class
    {
        //add the object to repository
        void Add(T entity);

        //Attach any modiied enttity to repository
        void Attach(T entity);

        void Delete(T entity);

        //Save changes for repository
        int SaveChanges();

        //Search from DB
        IQueryable<T> Fetch(bool isTrack = true);

        //return all object for type
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate, bool isTrack = true
            , IOrderByClause<T>[] orderBy = null, int skip = 0, int top = 0);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        //Implementation for single object save  
        void Save(T newEntity = null, T modifiedEntity = null, T deletedEntity = null, bool commitChanges = true);

        /// <summary>
        /// Implementation for collection of objects
        /// </summary>
        /// <param name="newEntity"></param>
        /// <param name="modifiedEntity"></param>
        void Save(List<T> newEntity = null, List<T> modifiedEntity = null, List<T> deletedEntity = null, bool commitChanges = true);


        Task<int> SaveChangesAsync();

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool isTrack = true
           , IOrderByClause<T>[] orderBy = null, int skip = 0, int top = 0);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        IEnumerable<T> ExecuteSqlQuery(string query, object[] parameter);

        Task<IEnumerable<T>> ExecuteSqlQueryAsync(string query, object[] parameter);

        Task<int> SaveAsync(T newEntity = null, T modifiedEntity = null, T deletedEntity = null, bool commitChanges = true);

        Task<int> SaveAsync(List<T> newEntity = null, List<T> modifiedEntity = null, List<T> deletedEntity = null, bool commitChanges = true);
    }
}
