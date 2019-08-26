using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using HMS.Service.Core.Interfaces;
using HMS.Web.Models;

namespace HMS.Service.Persistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext context;
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Adds a new entity of type TEntity 
        /// </summary>
        /// <param name="entity">The entity that you want to add</param>
        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Adds a list of entities of type TEntity
        /// </summary>
        /// <param name="entities">Range of entities</param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);
        }

        public TEntity Find(Guid? id)
        {
            return context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Gets a list of data which related to the expression 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            return context.Set<TEntity>().Where(expression);
        }

        /// <summary>
        /// Gets all data 
        /// </summary>
        /// <returns>A list of TEntity data</returns>
        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        /// <summary>
        /// Remove an entity by its id 
        /// </summary>
        /// <param name="id">The id or the key of the deleting data</param>
        public void Remove(Guid id)
        {
            var entity = context.Set<TEntity>().Find(id);
            context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Remove an entity data 
        /// </summary>
        /// <param name="entity">Entity data</param>
        public void Remove(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Remove a range of data 
        /// </summary>
        /// <param name="entities">List of entities</param>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity">The updated entity</param>
        /// <returns>true if update succeess</returns>
        public bool Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            return true;
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="Id">The id of an entity that is going to be updated</param>
        /// <param name="entity">The updated entity</param>
        /// <returns>true if update success</returns>
        public bool Update(Guid Id, TEntity entity)
        {
            var Entity = Find(Id);
            Entity = entity;
            context.Entry(Entity).State = EntityState.Modified;
            return true;
        }
    }
}