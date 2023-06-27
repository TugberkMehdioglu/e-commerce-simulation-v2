using Microsoft.EntityFrameworkCore;
using Project.DAL.ContextClasses;
using Project.DAL.Repositories.Abstarcts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Concretes
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly MyContext _context;

        public BaseRepository(MyContext context)
        {
            _context = context;
        }

        protected async void SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual async void Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            SaveAsync();
        }

        public virtual async void AddRange(ICollection<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            SaveAsync();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public virtual void Delete(T entity)
        {
            entity.Status = DataStatus.Deleted;
            entity.DeletedDate = DateTime.Now;
            SaveAsync();
        }

        public virtual void DeleteRange(ICollection<T> entities)
        {
            foreach (T entity in entities)
            {
                Delete(entity);
            }
        }

        public virtual void Destroy(T entity)
        {
            _context.Set<T>().Remove(entity);
            SaveAsync();
        }

        public virtual void DestroyRange(ICollection<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            SaveAsync();
        }

        public virtual async Task<T?> Find(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T?> FindFirstData()
        {
            return await _context.Set<T>().OrderBy(x => x.CreatedDate).FirstOrDefaultAsync();
        }

        public virtual async Task<T?> FindLastData()
        {
            return await _context.Set<T>().OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
        }

        public virtual async Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public virtual IQueryable<T> GetActives()
        {
            return Where(x => x.Status != DataStatus.Deleted);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual IQueryable<T> GetModifieds()
        {
            return Where(x => x.Status == DataStatus.Updated);
        }

        public virtual IQueryable<T> GetPassives()
        {
            return Where(x => x.Status == DataStatus.Deleted);
        }

        public virtual object Select(Expression<Func<T, object>> expression)
        {
            return _context.Set<T>().Select(expression);
        }

        public async Task<X?> SelectViaDTO<X>(Expression<Func<T, X>> expression) where X : class
        {
            return await _context.Set<T>().Select(expression).FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
            entity.Status = DataStatus.Updated;
            entity.ModifiedDate = DateTime.Now;
           
        }

        public void UpdateRange(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
    }
}
