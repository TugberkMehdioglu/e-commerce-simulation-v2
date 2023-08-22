using Project.BLL.ManagerServices.Abstracts;
using Project.DAL.Repositories.Abstarcts;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class ProductManager : BaseManager<Product>, IProductManager
    {
        private readonly IProductRepository _productRepository;
        public ProductManager(IRepository<Product> repository, IProductRepository productRepository) : base(repository)
        {
            _productRepository = productRepository;
        }

        public async Task<(bool, string?, List<Product>?)> GetCategoriesActiveProductsAsync(int categoryId)
        {
            List<Product> products;

            try
            {
                products = await _productRepository.GetCategoriesActiveProductsAsync(categoryId);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}. İçeriği => {exception.InnerException}", null);
            }

            return (true, null, products);
        }

        public async Task<(bool, string?, List<Product>?)> GetActiveProductsWithCategory()
        {
            List<Product> products;

            try
            {
                products = await _productRepository.GetActiveProductsWithCategory();
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}. İçeriği => {exception.InnerException}", null);
            }

            return (true, null, products);
        }

        public async Task<(bool, string?, Product?)> GetActiveProductWithAttributeAndCategoryAsync(int id)
        {
            Product? product;

            try
            {
                product = await _productRepository.GetActiveProductWithAttributeAndCategoryAsync(id);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}. İçeriği => {exception.InnerException}", null);
            }

            if (product == null) return (false, "Ürün bulunamadı", null);

            return (true, null, product);
        }
        public async Task<(bool, string?, List<Product>?)> GetActiveProductsWithCategory(string searchInProductName)
        {
            List<Product> products;
            try
            {
                products = await _productRepository.GetActiveProductsWithCategory(searchInProductName);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}. İçeriği => {exception.InnerException}", null);
            }

            return (true, null, products);
        }

        public async Task<(bool, string?, Product?)> GetActiveProductWithCategory(int id)
        {
            Product? product;
            try
            {
                product = await _productRepository.GetActiveProductWithCategory(id);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}. İçeriği => {exception.InnerException}", null);
            }

            if (product == null) return (false, "Ürün bulunamadı", null);
            else return (true, null, product);
        }
    }
}
