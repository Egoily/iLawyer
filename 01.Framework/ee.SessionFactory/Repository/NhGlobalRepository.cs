using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ee.SessionFactory.Repository
{
    public class NhGlobalRepository : IDisposable, IGlobalRepository
    {


        /// <summary>
        /// Session obtained in the constructor via the repository manger
        /// </summary>
        protected ISession session;

        /// <summary>
        /// constructor, gets the session from the repository manager.
        /// </summary>
        public NhGlobalRepository()
        {
            session = SessionManager.GetConnection();
        }


        /// <summary>
        /// Implementation of GetbyId using NHibernate
        /// </summary>
        /// <param name="id">the Id of the entity to look up</param>
        /// <returns></returns>
        public virtual TEntity GetById<TEntity>(object id)
        {
            return (session.Get<TEntity>(id));
        }

        /// <summary>
        /// Implementation of Create repository using Nhibernate, creates an entity in the DB
        /// </summary>
        /// <param name="entity">the entity to be created</param>
        /// <returns>The entity with the generated identifier</returns>
        public virtual TEntity Create<TEntity>(TEntity entity)
        {
            session.Save(entity);
            return (entity);
        }

        /// <summary>
        /// Implementation of Update repository using NHibernate, updates the entity in the DB
        /// </summary>
        /// <param name="entity">the entity to be updated</param>
        /// <returns>the updated entity</returns>
        public virtual TEntity Update<TEntity>(TEntity entity)
        {
            session.SaveOrUpdate(entity);
            return (entity);
        }

        /// <summary>
        /// Implementation of Delete Repository, Deletes an entity from the DB
        /// </summary>
        /// <param name="entity">the entity to be deleted</param>
        public virtual void Delete<TEntity>(TEntity entity)
        {
            session.Delete(entity);
        }



        /// <summary>
        /// Returns a QueryOver root query for the given entity with the expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual IQueryOver<TEntity, TEntity> GetQuery<TEntity>(Expression<Func<TEntity, bool>> expression = null)
            where TEntity : class
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
        public virtual IQueryOver<TEntity, TEntity> GetQuery<TEntity>(Expression<Func<TEntity>> alias, Expression<Func<TEntity, bool>> expression = null)
        where TEntity : class
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
        public virtual IQueryOver<TEntity, TEntity> GetQuery<TEntity>(List<ICriterion> criterions)
            where TEntity : class
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
        public virtual IEnumerable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> expression = null)
            where TEntity : class
        {
            if (expression == null)
            {
                return GetQuery<TEntity>().List();
            }
            else
            {
                return GetQuery<TEntity>().Where(expression).List();
            }

        }
        /// <summary>
        /// Returns a list of the given entity with the expression
        /// </summary>
        /// <param name="criterions"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Query<TEntity>(List<ICriterion> criterions)
            where TEntity : class
        {
            var query = GetQuery<TEntity>();
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
        /// Returns a list of the given entity with the IQueryOver by page
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryOver"></param>
        /// <param name="orderby"></param>
        /// <param name="isDesc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public virtual (IEnumerable<TEntity>, int?) QueryByPage<TEntity>(IQueryOver<TEntity, TEntity> queryOver, Expression<Func<TEntity, object>> orderby, bool isDesc = false, int pageIndex = 1, int pageSize = 0, PageQueryOption option = PageQueryOption.Both)
         where TEntity : class
        {
            IEnumerable<TEntity> list = null;
            int? totalCount = null;
            if (option == PageQueryOption.Both || option == PageQueryOption.Total)
            {
                totalCount = queryOver.RowCount();
            }
            switch (option)
            {
                case PageQueryOption.Both:
                    totalCount = queryOver.RowCount();
                    var orderQuery = queryOver.OrderBy(orderby);
                    var orderByQuery = isDesc ? orderQuery.Desc : orderQuery.Asc;
                    list = (pageIndex > 0 && pageSize > 0) ? orderByQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).List() : orderByQuery.List();
                    break;
                case PageQueryOption.Content:
                    orderQuery = queryOver.OrderBy(orderby);
                    orderByQuery = isDesc ? orderQuery.Desc : orderQuery.Asc;
                    list = (pageIndex > 0 && pageSize > 0) ? orderByQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).List() : orderByQuery.List();
                    break;
                case PageQueryOption.Total:
                    totalCount = queryOver.RowCount();
                    break;
                default:
                    break;
            }

            return (list, totalCount);
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
        public virtual (IEnumerable<TEntity>, int?) QueryByPage<TEntity>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc = false, int pageIndex = 1, int pageSize = 0, PageQueryOption option = PageQueryOption.Both)
            where TEntity : class
        {
            var queryOver = GetQuery(expression);
            return QueryByPage(queryOver, orderby, isDesc, pageIndex, pageSize, option);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criterions"></param>
        /// <param name="orderby"></param>
        /// <param name="isDesc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public virtual (IEnumerable<TEntity>, int?) QueryByPage<TEntity>(List<ICriterion> criterions, Expression<Func<TEntity, object>> orderby, bool isDesc = false, int pageIndex = 1, int pageSize = 0, PageQueryOption option = PageQueryOption.Both)
            where TEntity : class
        {
            var queryOver = GetQuery<TEntity>(criterions);
            return QueryByPage(queryOver, orderby, isDesc, pageIndex, pageSize, option);
        }


        public virtual void Dispose()
        {
            session?.Flush();
            SessionManager.CloseConnection();
        }


    }
}
