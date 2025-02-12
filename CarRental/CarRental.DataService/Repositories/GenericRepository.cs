using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CarRental.DataService.Data;
using CarRental.DataService.IRepositories;
using CarRental.Entities.DbSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarRental.DataService.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        //protected readonly ILogger _logger;

        internal DbSet<T> dbSet;

        public GenericRepository(AppDbContext context)//, ILogger logger)
        {
            _context = context;
            //_logger = logger;
            dbSet = context.Set<T>();
        }

        public virtual async Task<bool> AddAsync(T entity, User user)
        {
            return await dbSet.AddAsync(entity) is not null;
        }
        public virtual bool Update(T entity, User user)
        {
            return dbSet.Update(entity) is not null;
        }

        public virtual bool Delete(T entity)
        {
            return dbSet.Remove(entity) is not null;
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null!)
        {
            return await GenerateQuery(includes).Where(criteria).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
        {
            return await dbSet.Where(criteria).Skip(skip).Take(take).ToListAsync();
        }

        public virtual async Task<T> FindAsync(Guid id)
        {
            return await dbSet.FindAsync(id) ?? null!;
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null!)
        {
            return await GenerateQuery(includes).SingleOrDefaultAsync(criteria) ?? null!;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        private IQueryable<T> GenerateQuery(string[] includes)
        {
            IQueryable<T> query = dbSet;

            if (includes is not null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query;
        }
    }
}
