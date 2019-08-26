using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all data 
        /// </summary>
        /// <returns>A list of TEntity data</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Gets an by its id 
        /// </summary>
        /// <param name="id">The id of the wanted entity</param>
        /// <returns>a TEntity type data</returns>
        TEntity Find(Guid? id);

        /// <summary>
        /// Gets a list of data which related to the expression 
        /// </summary>
        /// <param name="expression">the expression of what you want</param>
        /// <returns>A List of TEntity type of data</returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Adds a new entity of type TEntity 
        /// </summary>
        /// <param name="entity">The entity that you want to add</param>
        void Add(TEntity entity);

        /// <summary>
        /// Adds a list of entities of type TEntity
        /// </summary>
        /// <param name="entities">Range of entities</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Remove an entity by its id 
        /// </summary>
        /// <param name="id">The id or the key of the deleting data</param>
        void Remove(Guid id);

        /// <summary>
        /// Remove an entity data 
        /// </summary>
        /// <param name="entity">Entity data</param>
        void Remove(TEntity entity);


        /// <summary>
        /// Remove a range of data 
        /// </summary>
        /// <param name="entities">List of entities</param>
        void RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity">The updated entity</param>
        /// <returns>true if update succeess</returns>
        bool Update(TEntity entity);

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="Id">The id of an entity that is going to be updated</param>
        /// <param name="entity">The updated entity</param>
        /// <returns>true if update success</returns>
        bool Update(Guid Id, TEntity entity);
    }

}
