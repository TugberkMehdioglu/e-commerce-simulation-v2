﻿using Project.DAL.ContextClasses;
using Project.DAL.Repositories.Abstarcts;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Concretes
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(MyContext context) : base(context)
        {
        }
    }
}
