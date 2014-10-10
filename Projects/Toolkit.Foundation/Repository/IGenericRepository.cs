using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LOGI.Framework.Toolkit.Foundation.Repository
{
    public interface IGenericRepository<T> where T : EntityBase
    {

        /// <summary>
        /// Return strongly typed IQueryable
        /// </summary>
        IQueryable<T> GetQuery();

        /// <summary>
        /// Load entity from the repository (always query store)
        /// </summary>
        /// <typeparam name="T">the entity type to load</typeparam>
        /// <param name="whereCondition">where condition</param>
        /// <returns>the loaded entity</returns>
        T Load(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// Provides explicit loading of object properties
        /// </summary>
        void LoadProperty(T entity, Expression<Func<T, object>> selector);

        /// <summary>
        /// Returns all entities for a given type
        /// </summary>
        /// <returns>All entities</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Returns all entities for a given type
        /// </summary>
        /// <returns>All entities</returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Add entity to the repository
        /// </summary>
        /// <param name="entity">the entity to add</param>
        /// <returns>The added entity</returns>
        void Add(T entity);

        /// <summary>
        /// Save all changes from repository to store
        /// </summary>
        /// <returns>Total number of objects affected</returns>
        int SaveChanges();

        /// <summary>
        /// Mark entity to be deleted within the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(T entity);

        #region Wcf RadGrid Methods

        ResultData<T> GetDataAndCount(int startRowIndex, int maximumRows, string sortExpression, string filterExpression) ;

        int GetDataCount(string filterExpression);
        #endregion
    }
}
