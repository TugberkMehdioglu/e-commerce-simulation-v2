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
        void Add(T entity);
        void AddRange(ICollection<T> entities);
        void Update(T entity);
        void UpdateRange(ICollection<T> entities);
        void Delete(T entity);
        void DeleteRange(ICollection<T> entities);
        void Destroy(T entity);
        void DestroyRange(ICollection<T> entities);

        //Expression Commands
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression);
        object Select(Expression<Func<T, object>> expression);
        Task<X?> SelectViaDTO<X>(Expression<Func<T, X>> expression) where X : class;//For DTO Classes without unboxing

        //Find Commands
        Task<T?> Find(int id);
        Task<T?> FindFirstData();
        Task<T?> FindLastData();
    }
}
