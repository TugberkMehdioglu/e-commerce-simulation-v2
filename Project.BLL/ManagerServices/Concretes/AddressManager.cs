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
    public class AddressManager : BaseManager<Address>, IAddressManager
    {
        public AddressManager(IRepository<Address> repository) : base(repository)
        {
        }
    }
}
