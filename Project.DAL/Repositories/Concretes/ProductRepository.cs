using Microsoft.EntityFrameworkCore;
using Project.DAL.ContextClasses;
using Project.DAL.Repositories.Abstarcts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Concretes
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(MyContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetCategoriesActiveProductsAsync(int categoryId) => await _context.Products!.Where(x => x.CategoryId == categoryId && x.Status != DataStatus.Deleted).Include(x => x.Category).ToListAsync();

        public async Task<List<Product>> GetActiveProductsWithCategory() => await _context.Products!.Where(x => x.Status != DataStatus.Deleted).Include(x => x.Category).ToListAsync();

        public async Task<Product?> GetActiveProductWithAttributeAndCategoryAsync(int id) => await _context.Products!.Where(x => x.Id == id && x.Status != DataStatus.Deleted).Include(x => x.ProductAttributes.Where(x => x.Status != DataStatus.Deleted)).Include(x => x.Category).FirstOrDefaultAsync();

        public async Task<List<Product>> GetActiveProductsWithCategory(string searchInProductName) => await _context.Products!.Where(x => x.Name.ToLower().Contains(searchInProductName.ToLower()) && x.Status != DataStatus.Deleted).Include(x => x.Category).ToListAsync();
    }
}
