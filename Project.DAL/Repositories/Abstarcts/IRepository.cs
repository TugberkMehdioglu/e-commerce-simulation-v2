using Project.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Abstarcts
{
    public interface IRepository<T> where T : IEntity
    {
        //List Commands
        IQueryable<T> GetAll();
        IQueryable<T> GetActives();
        IQueryable<T> GetModifieds();
        IQueryable<T> GetPassives();

        //Modify Commands
        Task AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(ICollection<T> entities);
        Task Destroy(T entity);
        Task DestroyRange(ICollection<T> entities);

        //Expression Commands
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        object Select(Expression<Func<T, object>> expression);
        Task<X?> SelectViaDTOAsync<X>(Expression<Func<T, X>> expression) where X : class;//For DTO Classes without unboxing

        //Find Commands
        Task<T?> FindAsync(int id);
        Task<T?> FindFirstDataAsync();
        Task<T?> FindLastDataAsync();
    }
}
