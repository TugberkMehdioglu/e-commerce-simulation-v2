using Microsoft.AspNetCore.Identity;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Abstracts
{
    public interface IAppUserManager : IManager<AppUser>
    {
        public Task<(bool, IEnumerable<IdentityError>?, string?)> AddUserByIdentityAsync(AppUser appUser);
        public Task<(bool, IEnumerable<IdentityError>?, string?)> ChangePasswordAsync(AppUser appUser, string formerPassword, string newPassword);
        public Task<AppUser?> FindByStringAsync(string Id);

        public Task<(bool, string?, AppUser?)> GetUserWithProfileAndAddressAsync(string userName);
    }
}
