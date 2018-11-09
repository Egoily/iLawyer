using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate.Criterion;

namespace ee.SessionFactory.Repository
{
    /// <summary>
    /// NHibernate generic repository, implements all basic CRUD operations. Manges the connection management
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity that this repository manages</typeparam>
    /// <typeparam name="TKey">The type of the key of the entity, tipically Int64 or Int32</typeparam>
    public class NhRepository<TEntity> : IDisposable, IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Session obtained in the constructor via the repository manger
        /// </summary>
        protected ISession session;

        /// <summary>
        /// constructor, gets the session from the repository manager.
        /// </summary>
        public NhRepository()
        {
            session = SessionManager.GetConnection();
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

        /// <summary>
        /// Returns a QueryOver root query for the given entity with the expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual IQueryOver<TEntity, TEntity> GetQuery(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return (session.QueryOver<TEntity>());
            }
            else
            {
                return (session.QueryOver<TEntity>().Where(expression));
            }

        }
        public virtual IQueryOver<TEntity, TEntity> GetQuery(Expression<Func<TEntity>> alias, Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return (session.QueryOver(alias));
            }
            else
            {
                return (session.QueryOver(alias).Where(expression));
            }

        }
        /// <summary>
        /// Returns a QueryOver root query for the given entity with the expression
        /// </summary>
        /// <param name="criterions"></param>
        /// <returns></returns>
        public virtual IQueryOver<TEntity, TEntity> GetQuery(List<ICriterion> criterions)
        {
            var query = (session.QueryOver<TEntity>());
            if (criterions != null)
            {
                foreach (var criterion in criterions)
                {
                    query = (query.Where(criterion));
                }
            }
            return query;

        }




        /// <summary>
        /// Returns a list of the given entity with the expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return GetQuery().List();
            }
            else
            {
                return GetQuery().Where(expression).List();
            }

        }
        /// <summary>
        /// Returns a list of the given entity with the expression
        /// </summary>
        /// <param name="criterions"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Query(List<ICriterion> criterions)
        {
            var query = GetQuery();
            if (criterions != null)
            {
                foreach (var criterion in criterions)
                {
                    query = query.Where(criterion);
                }
            }
            return query.List();
        }

        /// <summary>
        /// Returns a list of the given entity with the expression by page
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderby"></param>
        /// <param name="isDesc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>(list,totalCount)</returns>
        public virtual (IEnumerable<TEntity>, int) QueryByPage(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc = false, int pageIndex = 1, int pageSize = 0)
        {
            var query = GetQuery(expression);
            var totalCount = query.RowCount();
            var orderQuery = query.OrderBy(orderby);
            var orderByQuery = isDesc ? orderQuery.Desc : orderQuery.Asc;
            return (orderByQuery.Skip((pageIndex > 0 ? (pageIndex - 1) : 0) * (pageSize <= 0 ? totalCount : pageSize)).Take((pageSize <= 0 ? totalCount : pageSize)).List(), totalCount);
        }

        public virtual (IEnumerable<TEntity>, int) QueryByPage(List<ICriterion> criterions, Expression<Func<TEntity, object>> orderby, bool isDesc = false, int pageIndex = 1, int pageSize = 0)
        {
            var query = GetQuery(criterions);
            var totalCount = query.RowCount();
            var orderQuery = query.OrderBy(orderby);
            var orderByQuery = isDesc ? orderQuery.Desc : orderQuery.Asc;
            return (orderByQuery.Skip((pageIndex > 0 ? (pageIndex - 1) : 0) * (pageSize <= 0 ? totalCount : pageSize)).Take((pageSize <= 0 ? totalCount : pageSize)).List(), totalCount);
        }

        public virtual (IEnumerable<TEntity>, int) QueryByPage(IQueryOver<TEntity, TEntity> queryOver, Expression<Func<TEntity, object>> orderby, bool isDesc = false, int pageIndex = 1, int pageSize = 0)
        {

            var totalCount = queryOver.RowCount();
            var orderQuery = queryOver.OrderBy(orderby);
            var orderByQuery = isDesc ? orderQuery.Desc : orderQuery.Asc;
            return (orderByQuery.Skip((pageIndex > 0 ? (pageIndex - 1) : 0) * (pageSize <= 0 ? totalCount : pageSize)).Take((pageSize <= 0 ? totalCount : pageSize)).List(), totalCount);
        }

        public virtual void Dispose()
        {

            session?.Flush();
            if (session?.IsOpen ?? false)
                session?.Close();
        }


    }
}
