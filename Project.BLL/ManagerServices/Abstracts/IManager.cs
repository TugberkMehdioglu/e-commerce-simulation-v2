using Project.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Abstracts
{
    public interface IManager<T> where T : IEntity
    {
        //List Commands
        IQueryable<T> GetAll();
        IQueryable<T> GetActives();
        IQueryable<T> GetModifieds();
        IQueryable<T> GetPassives();

        //Modify Commands
        (bool, string?) AddAsync(T entity);
        (bool, string?) AddRangeAsync(ICollection<T> entities);
        (bool, string?) UpdateAsync(T entity);
        (bool, string?) UpdateRangeAsync(ICollection<T> entities);
        (bool, string?) DeleteAsync(T entity);
        (bool, string?) DeleteRangeAsync(ICollection<T> entities);
        (bool, string?) Destroy(T entity);
        (bool, string?) DestroyRange(ICollection<T> entities);

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
