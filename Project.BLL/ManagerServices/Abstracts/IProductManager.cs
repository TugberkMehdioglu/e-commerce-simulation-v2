using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Abstracts
{
    public interface IProductManager : IManager<Product>
    {
        public Task<(bool, string?, List<Product>?)> GetCategoriesActiveProductsAsync(int categoryId);
        public Task<(bool, string?, List<Product>?)> GetActiveProductsWithCategory();
        public Task<(bool, string?, Product?)> GetActiveProductWithAttributeAndCategoryAsync(int id);
        public Task<(bool, string?, List<Product>?)> GetActiveProductsWithCategory(string searchInProductName);
        public Task<(bool, string?, Product?)> GetActiveProductWithCategory(int id);
    }
}
