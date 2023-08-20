using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Abstarcts
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<List<Product>> GetCategoriesActiveProductsAsync(int categoryId);
        public Task<List<Product>> GetActiveProductsWithCategory();
        public Task<Product?> GetActiveProductWithAttributeAndCategoryAsync(int id);
        public Task<List<Product>> GetActiveProductsWithCategory(string searchInProductName);
    }
}
