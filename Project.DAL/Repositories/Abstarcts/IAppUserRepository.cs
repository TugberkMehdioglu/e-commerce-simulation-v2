using Microsoft.AspNetCore.Identity;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Abstarcts
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        public Task<(bool, IEnumerable<IdentityError>?)> AddUserAsync(AppUser appUser);
        public Task<(bool, IEnumerable<IdentityError>?)> ChangePasswordAsync(AppUser appUser, string formerPassword, string newPassword);
        public Task<AppUser?> FindByStringAsync(string Id);
        public Task<AppUser?> GetUserWithProfileAndAddressAsync(string userName);
        public Task<AppUser?> GetUserWithProfileAsync(string userName);
    }
}
