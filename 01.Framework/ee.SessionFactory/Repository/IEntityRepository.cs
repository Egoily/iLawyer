using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ee.SessionFactory.Repository
{
    /// <summary>
    /// Interface to entity repository, contains the basic CRUD methods that entity repository must implement
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of entity that the repository implemntation will manage
    /// </typeparam>
    /// <typeparam name="TKey">The type of the key that the repository will manage</typeparam>
    public interface IEntityRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets an entity by it's unique ID
        /// </summary>
        /// <param name="id">Id of the entity to retrieve</param>
        /// <returns>The updated entity</returns>
        TEntity GetById(object id);
        /// <summary>
        /// Inserts an entity into the repository
        /// </summary>
        /// <param name="entity">the entity to be stored</param>
        /// <returns>the updated entity</returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// Updates an entity in the repository
        /// </summary>
        /// <param name="entity">the entity to be updated</param>
        /// <returns>the</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Deletes an entity from the repository
        /// </summary>
        /// <param name="entity">the entity to be deleted</param>
        void Delete(TEntity entity);


        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> expression = null, bool cacheable = false);
        IEnumerable<TEntity> QueryInCriterion(List<ICriterion> criterions, bool cacheable = false);
        (IEnumerable<TEntity>, int?) QueryByPage(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc = false, int pageIndex = 1, int pageSize = 0, PageQueryOption option = PageQueryOption.Both, bool cacheable = false);
        (IEnumerable<TEntity>, int?) QueryByPageInCriterion(List<ICriterion> criterions, Expression<Func<TEntity, object>> orderby, bool isDesc = false, int pageIndex = 1, int pageSize = 0, PageQueryOption option = PageQueryOption.Both, bool cacheable = false);

    }
}
