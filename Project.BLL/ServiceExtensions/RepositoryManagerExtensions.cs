using Microsoft.Extensions.DependencyInjection;
using Project.BLL.ManagerServices.Abstracts;
using Project.BLL.ManagerServices.Concretes;
using Project.DAL.Repositories.Abstarcts;
using Project.DAL.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ServiceExtensions
{
    public static class RepositoryManagerExtensions
    {
        public static IServiceCollection AddRepositoryManagerServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAppUserProfileRepository, AppUserProfileRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();


            services.AddScoped(typeof(IManager<>), typeof(BaseManager<>));
            services.AddScoped<IAddressManager, AddressManager>();
            services.AddScoped<IAppUserManager, AppUserManager>();
            services.AddScoped<IAppUserProfileManager, AppUserProfileManager>();
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<IOrderDetailManager, OrderDetailManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IProductAttributeManager, ProductAttributeManager>();
            services.AddScoped<IProductManager, ProductManager>();

            return services;
        }
    }
}
