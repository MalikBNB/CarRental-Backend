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


        public async Task<bool> AnyAsync(Expression<Func<T, bool>> criteria)
        {
            return await dbSet.AnyAsync(criteria);
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            try
            {
                return await dbSet.AddAsync(entity) is not null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual bool Update(T entity)
        {
            try
            {
                return dbSet.Update(entity) is not null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                return await Task.Run(() => Update(entity));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual bool Delete(T entity)
        {
            try
            {
                return dbSet.Remove(entity) is not null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null!)
        {
            return await GenerateQuery(includes, null, null).Where(criteria).AsNoTracking().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? page, int? pageSize, string[] includes = null!)
        {
            return await GenerateQuery(includes, page, pageSize).Where(criteria).AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> FindAsync(Guid id)
        {
            return await dbSet.FindAsync(id) ?? null!;
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null!)
        {
            return await GenerateQuery(includes, null, null).AsNoTracking().SingleOrDefaultAsync(criteria) ?? null!;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(string[] includes = null!)
        {
            return await GenerateQuery(includes, null, null).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(int? page, int? pageSize, string[] includes = null!)
        {
            return await GenerateQuery(includes, page, pageSize).AsNoTracking().ToListAsync();
        }

        private IQueryable<T> GenerateQuery(string[] includes, int? page, int? pageSize)
        {
            IQueryable<T> query = dbSet;

            if (includes is not null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (pageSize.HasValue)
                query = query.Take(pageSize.Value);

            if (page.HasValue)
                query = query.Skip((page.Value - 1) * pageSize.Value);

            return query;
        }


    }
}
