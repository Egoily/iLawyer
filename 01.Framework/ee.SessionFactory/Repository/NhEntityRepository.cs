using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ee.SessionFactory.Repository
{
    /// <summary>
    /// NHibernate generic repository, implements all basic CRUD operations. Manges the connection management
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that this repository manages</typeparam>
    /// <typeparam name="TKey">The type of the key of the entity, tipically Int64 or Int32</typeparam>
    public class NhEntityRepository<TEntity> : IDisposable, IEntityRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Session obtained in the constructor via the repository manger
        /// </summary>
        protected ISession session;

        /// <summary>
        /// constructor, gets the session from the repository manager.
        /// </summary>
        public NhEntityRepository()
        {
            session = SessionManager.GetConnection();
        }
        public virtual void Dispose()
        {
            session?.Flush();
            SessionManager.CloseConnection();
        }



        /// <summary>
        /// Implementation of GetbyId using NHibernate
        /// </summary>
        /// <param name="id">the Id of the entity to look up</param>
        /// <returns></returns>
        public virtual TEntity GetById(object id)
        {
            return (session.Get<TEntity>(id));
        }

        /// <summary>
        /// Implementation of Create repository using Nhibernate, creates an entity in the DB
        /// </summary>
        /// <param name="entity">the entity to be created</param>
        /// <returns>The entity with the generated identifier</returns>
        public virtual TEntity Create(TEntity entity)
        {
            session.Save(entity);
            return (entity);
        }

        /// <summary>
        /// Implementation of Update repository using NHibernate, updates the entity in the DB
        /// </summary>
        /// <param name="entity">the entity to be updated</param>
        /// <returns>the updated entity</returns>
        public virtual TEntity Update(TEntity entity)
        {
            session.SaveOrUpdate(entity);
            return (entity);
        }

        /// <summary>
        /// Implementation of Delete Repository, Deletes an entity from the DB
        /// </summary>
        /// <param name="entity">the entity to be deleted</param>
        public virtual void Delete(TEntity entity)
        {
            session.Delete(entity);
        }

        public virtual IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> expression = null, bool cacheable = false)
        {
            var query = GetQueryOver(expression, cacheable);
            return query.List();
        }

        public virtual IEnumerable<TEntity> QueryInCriterion(List<ICriterion> criterions, bool cacheable = false)
        {
            var query = GetQueryOverInCriterion(criterions, cacheable);
            return query.List();
        }


        public virtual (IEnumerable<TEntity>, int?) QueryByPage(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc = false, int pageIndex = 1, int pageSize = 0, PageQueryOption option = PageQueryOption.Both, bool cacheable = false)
        {
            var queryOver = GetQueryOver(expression);
            return QueryByPageInternal(queryOver, orderby, isDesc, pageIndex, pageSize, option, cacheable);
        }

        public virtual (IEnumerable<TEntity>, int?) QueryByPageInCriterion(List<ICriterion> criterions, Expression<Func<TEntity, object>> orderby, bool isDesc = false, int pageIndex = 1, int pageSize = 0, PageQueryOption option = PageQueryOption.Both, bool cacheable = false)
        {
            var queryOver = GetQueryOverInCriterion(criterions);
            return QueryByPageInternal(queryOver, orderby, isDesc, pageIndex, pageSize, option, cacheable);
        }









        public virtual IQueryOver<TEntity, TEntity> GetQueryOver(Expression<Func<TEntity>> alias, Expression<Func<TEntity, bool>> expression = null, bool cacheable = false)
        {
            IQueryOver<TEntity, TEntity> queryOver;

            if (alias != null)
            {
                queryOver = session.QueryOver(alias);
            }
            else
            {
                queryOver = session.QueryOver<TEntity>();
            }
            if (expression != null)
            {
                queryOver = queryOver.Where(expression);
            }
            return queryOver;
        }

        public virtual IQueryOver<TEntity, TEntity> GetQueryOver(Expression<Func<TEntity, bool>> expression = null, bool cacheable = false)
        {
            return GetQueryOver(null, expression, cacheable);
        }


        public virtual IQueryOver<TEntity, TEntity> GetQueryOverInCriterion(List<ICriterion> criterions, bool cacheable = false)
        {
            var query = (session.QueryOver<TEntity>());
            if (criterions != null && criterions.Any())
            {
                foreach (var criterion in criterions)
                {
                    query = (query.Where(criterion));
                }
            }
            return query;

        }



        private static (IEnumerable<TEntity>, int?) QueryByPageInternal(IQueryOver<TEntity, TEntity> queryOver, Expression<Func<TEntity, object>> orderby, bool isDesc = false, int pageIndex = 1, int pageSize = 0, PageQueryOption option = PageQueryOption.Both, bool cacheable = false)
        {
            IEnumerable<TEntity> list = null;
            int? totalCount = null;


            switch (option)
            {
                case PageQueryOption.Both:
                    totalCount = RowCountInternal(queryOver, cacheable);
                    list = QueryInternal(queryOver, orderby, isDesc, pageIndex, pageSize, cacheable);
                    break;
                case PageQueryOption.Content:
                    list = QueryInternal(queryOver, orderby, isDesc, pageIndex, pageSize, cacheable);
                    break;
                case PageQueryOption.Total:
                    totalCount = RowCountInternal(queryOver, cacheable);
                    break;
                default:
                    break;
            }

            return (list, totalCount);
        }

        private static int? RowCountInternal(IQueryOver<TEntity, TEntity> queryOver, bool cacheable)
        {
            return cacheable ? queryOver.Cacheable().CacheMode(CacheMode.Normal).RowCount() : queryOver.RowCount();
        }

        private static IEnumerable<TEntity> QueryInternal(IQueryOver<TEntity, TEntity> queryOver, Expression<Func<TEntity, object>> orderby, bool isDesc, int pageIndex, int pageSize, bool cacheable)
        {
            if (pageIndex > 0 && pageSize > 0)
            {
                var orderQuery = queryOver.OrderBy(orderby);
                var orderByQuery = isDesc ? orderQuery.Desc : orderQuery.Asc;

                return cacheable ? orderByQuery.Cacheable().CacheMode(CacheMode.Normal).Skip((pageIndex - 1) * pageSize).Take(pageSize).List() : orderByQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).List();


            }
            else
            {
                return cacheable ? queryOver.Cacheable().List() : queryOver.List();
            }
        }








    }
}
