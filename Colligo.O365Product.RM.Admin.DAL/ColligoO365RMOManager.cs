using Colligo.O365Product.RM.Admin.Data;
using Colligo.O365Product.RM.Admin.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Colligo.O365Product.RM.Admin.DAL
{
    public class ColligoO365RMOManager<T> : DataManager<ColligoO365RMOManager<T>> where T : class
    {
        /// <summary>
        /// find data asyncly
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="isTrack"></param>
        /// <param name="orderBy"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, bool isTrack = true
             , IOrderByClause<T>[] orderBy = null, int skip = 0, int take = 0)
        {
            using (DataRepository<T> repoSystemCode = new DataRepository<T>(new ColligoO365RMOEntities()))
            {
                return await repoSystemCode.FindAsync(predicate, isTrack, orderBy, skip, take);
            }
        }

        /// <summary>
        /// find data sync
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="isTrack"></param>
        /// <param name="orderBy"></param>
        /// <param name="skip"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static IList<T> Find(Expression<Func<T, bool>> predicate, bool isTrack = true
            , IOrderByClause<T>[] orderBy = null, int skip = 0, int top = 0)
        {
            using (DataRepository<T> repoSystemCode = new DataRepository<T>(new ColligoO365RMOEntities()))
            {
                return repoSystemCode.Find(predicate, isTrack, orderBy, skip, top).ToList();
            }
        }

        /// <summary>
        /// get first record asyncsly
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            using (DataRepository<T> repoSystemCode = new DataRepository<T>(new ColligoO365RMOEntities()))
            {
                return await repoSystemCode.FirstOrDefaultAsync(predicate);
            }
        }

        /// <summary>
        /// get first record
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            using (DataRepository<T> repoSystemCode = new DataRepository<T>(new ColligoO365RMOEntities()))
            {
                return repoSystemCode.FirstOrDefault(predicate);
            }
        }

        /// <summary>
        /// get single record
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            using (DataRepository<T> repoSystemCode = new DataRepository<T>(new ColligoO365RMOEntities()))
            {
                return await repoSystemCode.SingleOrDefaultAsync(predicate);
            }
        }

        /// <summary>
        /// get single record
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            using (DataRepository<T> repoSystemCode = new DataRepository<T>(new ColligoO365RMOEntities()))
            {
                return repoSystemCode.SingleOrDefault(predicate);
            }
        }

        /// <summary>
        /// execute sql query async
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ExecuteSqlQueryAsync(string query, object[] parameter)
        {
            using (DataRepository<T> repoSystemCode = new DataRepository<T>(new ColligoO365RMOEntities()))
            {
                return await repoSystemCode.ExecuteSqlQueryAsync(query, parameter);
            }

        }

        /// <summary>
        /// Execute sql query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static List<T> ExecuteSqlQuery(string query, params object[] parameter)
        {
            using (DataRepository<T> repoSystemCode = new DataRepository<T>(new ColligoO365RMOEntities()))
            {
                return repoSystemCode.ExecuteSqlQuery(query, parameter).ToList();
            }

        }

        /// <summary>
        /// Get max data row for orderby Id
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        public static async Task<T> MaxAsync(IOrderByClause<T> orderBy, bool isTrack = true)
        {
            using (DataRepository<T> repoSystemCode = new DataRepository<T>(new ColligoO365RMOEntities()))
            {
                return await repoSystemCode.MaxAsync(orderBy, isTrack);
            }
        }


        /// <summary>
        /// Save Records
        /// </summary>
        /// <param name="newEntity"></param>
        /// <param name="modifiedEntity"></param>
        /// <param name="commitChanges"></param>
        public static void Save(T newEntity = null, T modifiedEntity = null, T deletedEntity = null, bool commitChanges = true)
        {
            using (DataRepository<T> repoSystemCode = new DataRepository<T>(new ColligoO365RMOEntities()))
            {
                repoSystemCode.Save(newEntity, modifiedEntity, deletedEntity, commitChanges);
            }
        }

        /// <summary>
        /// Save multiple list
        /// </summary>
        /// <param name="newEntity"></param>
        /// <param name="modifiedEntity"></param>
        /// <param name="commitChanges"></param>
        public static void Save(List<T> newEntity, List<T> modifiedEntity = null, List<T> deletedEntity = null, bool commitChanges = true)
        {
            using (DataRepository<T> repoSystemCode = new DataRepository<T>(new ColligoO365RMOEntities()))
            {
                repoSystemCode.Save(newEntity, modifiedEntity, deletedEntity, commitChanges);
            }

        }

        /// <summary>
        /// Save Sync method
        /// </summary>
        /// <param name="newEntity"></param>
        /// <param name="modifiedEntity"></param>
        /// <param name="commitChanges"></param>
        /// <returns></returns>
        public static async Task<int> SaveAsync(T newEntity = null, T modifiedEntity = null, T deletedEntity = null, bool commitChanges = true)
        {
            using (DataRepository<T> repoSystemCode = new DataRepository<T>(new ColligoO365RMOEntities()))
            {
                return await repoSystemCode.SaveAsync(newEntity, modifiedEntity, deletedEntity, commitChanges);
            }
        }

        /// <summary>
        /// Save Async method
        /// </summary>
        /// <param name="newEntity"></param>
        /// <param name="modifiedEntity"></param>
        /// <param name="commitChanges"></param>
        /// <returns></returns>
        public static async Task<int> SaveAsync(List<T> newEntity, List<T> modifiedEntity = null, List<T> deletedEntity = null, bool commitChanges = true)
        {
            using (DataRepository<T> repoSystemCode = new DataRepository<T>(new ColligoO365RMOEntities()))
            {
                return await repoSystemCode.SaveAsync(newEntity, modifiedEntity, deletedEntity, commitChanges);
            }
        }
    }

}
