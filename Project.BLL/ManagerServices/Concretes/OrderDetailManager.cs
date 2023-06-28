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
    public class OrderDetailManager : BaseManager<OrderDetail>, IOrderDetailManager
    {
        public OrderDetailManager(IRepository<OrderDetail> repository) : base(repository)
        {
        }
    }
}
