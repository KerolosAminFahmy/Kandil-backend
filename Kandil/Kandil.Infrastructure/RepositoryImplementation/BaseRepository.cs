using Kandil.Application.ConstantOrEnum;
using Kandil.Application.RepositoryInterfaces;
using Kandil.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kandil.Infrastructure.RepositoryImplementation
{
    internal class BaseRepository<T>(ApplicationDbContext _context) : IBaseRepository<T> where T : class
    {

        protected DbSet<T> db = _context.Set<T>();
        /// <summary>
        /// Gets all elements.
        /// </summary>
        /// <returns>An IEnumerable of all elements.</returns>
        public IEnumerable<T> GetAll()
        {
            return db.ToList();
        }
        /// <summary>
        /// Asynchronously gets all elements.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an IEnumerable of all elements.</returns>

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await db.ToListAsync();
        }
        /// <summary>
        /// Gets an element by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the element.</param>
        /// <returns>The element with the specified identifier.</returns>
        public T GetById(int id)
        {
            return db.Find(id);
        }
        /// <summary>
        /// Asynchronously gets an Element by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the element.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity with the specified identifier.</returns>

        public async Task<T> GetByIdAsync(int id)
        {
            return await db.FindAsync(id);
        }
        /// <summary>
        /// Finds an element based on a given condition.
        /// </summary>
        /// <param name="criteria">The criteria to match elements.</param>
        /// <param name="includes">Optional related elements to include in the query.</param>
        /// <returns>The first element that matches the given criteria.</returns>
        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = db;

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return query.SingleOrDefault(criteria);
        }
        /// <summary>
        /// Asynchronously finds an entity based on a given condition.
        /// </summary>
        /// <param name="criteria">The criteria to match entities.</param>
        /// <param name="includes">Optional related entities to include in the query.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the first entity that matches the given criteria.</returns>
        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = db;

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.SingleOrDefaultAsync(criteria);
        }
        /// <summary>
        /// Finds all entities based on a given condition.
        /// </summary>
        /// <param name="criteria">The criteria to match entities.</param>
        /// <param name="includes">Optional related entities to include in the query.</param>
        /// <returns>An IEnumerable of all entities that match the given criteria.</returns>
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = db;

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(criteria).ToList();
        }
        /// <summary>
        /// Finds a subset of entities based on a condition with pagination.
        /// </summary>
        /// <param name="criteria">The criteria to match entities.</param>
        /// <param name="take">The number of records to return.</param>
        /// <param name="skip">The number of records to skip.</param>
        /// <returns>An IEnumerable of the entities that match the given criteria with pagination.</returns>

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return db.Where(criteria).Skip(skip).Take(take).ToList();
        }
        /// <summary>
        /// Finds all entities based on a condition with optional sorting and pagination.
        /// </summary>
        /// <param name="criteria">The criteria to match entities.</param>
        /// <param name="take">Optional number of records to return.</param>
        /// <param name="skip">Optional number of records to skip.</param>
        /// <param name="orderBy">Optional sorting expression.</param>
        /// <param name="orderByDirection">The sorting direction, either Ascending or Descending.</param>
        /// <returns>An IEnumerable of the entities that match the given criteria with optional sorting and pagination.</returns>

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = db.Where(criteria);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query.ToList();
        }
        /// <summary>
        /// Asynchronously finds all entities based on a condition with optional related entities.
        /// </summary>
        /// <param name="criteria">The criteria to match entities.</param>
        /// <param name="includes">Optional related entities to include in the query.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an IEnumerable of all matching entities.</returns>

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = db;

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(criteria).ToListAsync();
        }
        /// <summary>
        /// Asynchronously finds a subset of entities based on a condition with pagination.
        /// </summary>
        /// <param name="criteria">The criteria to match entities.</param>
        /// <param name="skip">The number of records to skip.</param>
        /// <param name="take">The number of records to return.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entities with pagination.</returns>

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
        {
            return await db.Where(criteria).Skip(skip).Take(take).ToListAsync();
        }
        /// <summary>
        /// Asynchronously finds all entities based on a condition with optional sorting and pagination.
        /// </summary>
        /// <param name="criteria">The criteria to match entities.</param>
        /// <param name="take">Optional number of records to return.</param>
        /// <param name="skip">Optional number of records to skip.</param>
        /// <param name="orderBy">Optional sorting expression.</param>
        /// <param name="orderByDirection">The sorting direction, either Ascending or Descending.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the matching entities with optional sorting and pagination.</returns>

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = db.Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();
        }
        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        public T Add(T entity)
        {
            db.Add(entity);
            return entity;
        }
        /// <summary>
        /// Asynchronously adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>

        public async Task<T> AddAsync(T entity)
        {
            await db.AddAsync(entity);
            return entity;
        }
        /// <summary>
        /// Adds a range of new entities.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>An IEnumerable of the added entities.</returns>
        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            db.AddRange(entities);
            return entities;
        }
        /// <summary>
        /// Asynchronously adds a range of new entities.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entities.</returns>

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await db.AddRangeAsync(entities);
            return entities;
        }
        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The updated entity.</returns>
        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }
        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void Delete(T entity)
        {
            db.Remove(entity);
        }
        /// <summary>
        /// Deletes a range of entities.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        public void DeleteRange(IEnumerable<T> entities)
        {
            db.RemoveRange(entities);
        }
        /// <summary>
        /// Gets the total count of entities.
        /// </summary>
        /// <returns>The total count of entities.</returns>
        public int Count()
        {
            return db.Count();
        }
        /// <summary>
        /// Gets the count of entities that match a condition.
        /// </summary>
        /// <param name="criteria">The criteria to match entities.</param>
        /// <returns>The count of entities that match the condition.</returns>
        public int Count(Expression<Func<T, bool>> criteria)
        {
            return db.Count(criteria);
        }
        /// <summary>
        /// Asynchronously gets the total count of entities.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the total count of entities.</returns>

        public async Task<int> CountAsync()
        {
            return await db.CountAsync();
        }
        // <summary>
        /// Asynchronously gets the count of entities that match a condition.
        /// </summary>
        /// <param name="criteria">The criteria to match entities.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the count of entities that match the condition.</returns>
        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await db.CountAsync(criteria);
        }
    }
}
