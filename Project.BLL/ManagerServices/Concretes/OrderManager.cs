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
    public class OrderManager : BaseManager<Order>, IOrderManager
    {
        public OrderManager(IRepository<Order> repository) : base(repository)
        {
        }
    }
}
