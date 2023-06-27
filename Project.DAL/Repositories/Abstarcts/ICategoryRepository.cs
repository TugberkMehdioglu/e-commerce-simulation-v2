using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Abstarcts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public IQueryable<Category> GetActiveQueryableCategories();
    }
}
