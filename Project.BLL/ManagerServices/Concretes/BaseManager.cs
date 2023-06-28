using Project.BLL.ManagerServices.Abstracts;
using Project.DAL.Repositories.Abstarcts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class BaseManager<T> : IManager<T> where T : class, IEntity
    {
        private readonly IRepository<T> _repository;

        public BaseManager(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual (bool, string?) AddAsync(T entity)
        {
            if (entity == null || entity.Status == DataStatus.Deleted) return (false, "Lütfen gerekli alanları doldurun");

            try
            {
                _repository.AddAsync(entity);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}, İçeriği => {exception.InnerException}");
            }

            return (true, null);
        }

        public virtual (bool, string?) AddRangeAsync(ICollection<T> entities)
        {
            if(entities == null || entities.Count == 0) return (false, "Lütfen gerekli alanları doldurun");

            try
            {
                _repository.AddRangeAsync(entities);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}, İçeriği => {exception.InnerException}");
            }

            return (true, null);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public virtual (bool, string?) DeleteAsync(T entity)
        {
            if (entity == null || entity.Status == DataStatus.Deleted) return (false, "Lütfen gerekli alanları doldurun");

            try
            {
                _repository.DeleteAsync(entity);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}, İçeriği => {exception.InnerException}");
            }

            return (true, null);
        }

        public virtual (bool, string?) DeleteRangeAsync(ICollection<T> entities)
        {
            if (entities == null || entities.Count == 0) return (false, "Lütfen gerekli alanları doldurun");

            try
            {
                _repository.DeleteRangeAsync(entities);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}, İçeriği => {exception.InnerException}");
            }

            return (true, null);
        }

        public virtual (bool, string?) Destroy(T entity)
        {
            if (entity == null || entity.Status == DataStatus.Deleted) return (false, "Lütfen gerekli alanları doldurun");

            try
            {
                _repository.Destroy(entity);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}, İçeriği => {exception.InnerException}");
            }

            return (true, null);
        }

        public virtual (bool, string?) DestroyRange(ICollection<T> entities)
        {
            if (entities == null || entities.Count == 0) return (false, "Lütfen gerekli alanları doldurun");

            try
            {
                _repository.DestroyRange(entities);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}, İçeriği => {exception.InnerException}");
            }

            return (true, null);
        }

        public virtual async Task<T?> FindAsync(int id)
        {
            return await _repository.FindAsync(id);
        }

        public virtual async Task<T?> FindFirstDataAsync()
        {
            return await _repository.FindFirstDataAsync();
        }

        public virtual async Task<T?> FindLastDataAsync()
        {
            return await _repository.FindLastDataAsync();
        }

        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.FirstOrDefaultAsync(expression);
        }

        public virtual IQueryable<T> GetActives()
        {
            return _repository.GetActives();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual IQueryable<T> GetModifieds()
        {
            return _repository.GetModifieds();
        }

        public virtual IQueryable<T> GetPassives()
        {
            return _repository.GetPassives();
        }

        public virtual object Select(Expression<Func<T, object>> expression)
        {
            return _repository.Select(expression);
        }

        public virtual async Task<X?> SelectViaDTOAsync<X>(Expression<Func<T, X>> expression) where X : class
        {
            return await _repository.SelectViaDTOAsync(expression);
        }

        public virtual (bool, string?) UpdateAsync(T entity)
        {
            if (entity == null || entity.Status == DataStatus.Deleted) return (false, "Lütfen gerekli alanları doldurun");

            try
            {
                _repository.UpdateAsync(entity);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}, İçeriği => {exception.InnerException}");
            }

            return (true, null);
        }

        public virtual (bool, string?) UpdateRangeAsync(ICollection<T> entities)
        {
            if (entities == null || entities.Count == 0) return (false, "Lütfen gerekli alanları doldurun");

            try
            {
                _repository.UpdateRangeAsync(entities);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}, İçeriği => {exception.InnerException}");
            }

            return (true, null);
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
