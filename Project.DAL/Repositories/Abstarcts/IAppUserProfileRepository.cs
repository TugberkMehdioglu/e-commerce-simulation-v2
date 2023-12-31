﻿using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Abstarcts
{
    public interface IAppUserProfileRepository : IRepository<AppUserProfile>
    {
        public Task<AppUserProfile?> FindByStringAsync(string Id);
    }
}
