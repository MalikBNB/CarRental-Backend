using CarRental.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataService.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string[] includes = null!);
        Task<IEnumerable<T>> GetAllAsync(int skip, int take, string[] includes = null!);
        Task<bool> AnyAsync(Expression<Func<T, bool>> criteria);
        Task<T> FindAsync(Guid id);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null!);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null!);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take, string[] includes = null!);
        Task<bool> AddAsync(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
