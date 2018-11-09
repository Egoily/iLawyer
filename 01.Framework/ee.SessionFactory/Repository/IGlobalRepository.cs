using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ee.SessionFactory.Repository
{
    /// <summary>
    /// Interface to any repository, contains the basic CRUD methods that any repository must implement
    /// </summary>
    public interface IGlobalRepository
    {
        /// <summary>
        /// Gets an entity by it's unique ID
        /// </summary>
        /// <param name="id">Id of the entity to retrieve</param>
        /// <returns>The updated entity</returns>
        TEntity GetById<TEntity>(object id);

        /// <summary>
        /// Inserts an entity into the repository
        /// </summary>
        /// <param name="entity">the entity to be stored</param>
        /// <returns>the updated entity</returns>
        TEntity Create<TEntity>(TEntity entity);

        /// <summary>
        /// Updates an entity in the repository
        /// </summary>
        /// <param name="entity">the entity to be updated</param>
        /// <returns>the</returns>
        TEntity Update<TEntity>(TEntity entity);

        /// <summary>
        /// Deletes an entity from the repository
        /// </summary>
        /// <param name="entity">the entity to be deleted</param>
        void Delete<TEntity>(TEntity entity);

        IEnumerable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> expression = null) where TEntity : class;
        IEnumerable<TEntity> Query<TEntity>(List<ICriterion> criterions) where TEntity : class;
        (IEnumerable<TEntity>, int) QueryByPage<TEntity>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc = false, int pageIndex = 1, int pageSize = 0) where TEntity : class;
        (IEnumerable<TEntity>, int) QueryByPage<TEntity>(List<ICriterion> criterions, Expression<Func<TEntity, object>> orderby, bool isDesc = false, int pageIndex = 1, int pageSize = 0) where TEntity : class;


    }
}
