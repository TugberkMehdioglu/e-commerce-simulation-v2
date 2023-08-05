using Microsoft.AspNetCore.Identity;
using Project.BLL.ManagerServices.Abstracts;
using Project.DAL.Repositories.Abstarcts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class AppUserManager : BaseManager<AppUser>, IAppUserManager
    {
        private readonly IAppUserRepository _appUserRepository;
        public AppUserManager(IRepository<AppUser> repository, IAppUserRepository appUserRepository) : base(repository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<(bool, IEnumerable<IdentityError>?, string?)> AddUserByIdentityAsync(AppUser appUser)
        {
            if (appUser == null || appUser.Status == DataStatus.Deleted || appUser.PasswordHash == null || appUser.Email == null || appUser.PhoneNumber == null || appUser.UserName == null) return (false, null, "Lütfen zorunlu alanları doldurun");

            var (isSuccess, errors) = await _appUserRepository.AddUserAsync(appUser);

            if (isSuccess) return (true, null, null);
            return (false, errors, null);
        }

        public async Task<(bool, IEnumerable<IdentityError>?, string?)> ChangePasswordAsync(AppUser appUser, string formerPassword, string newPassword)
        {
            bool isSuccess;
            IEnumerable<IdentityError>? errors;

            try
            {
                (isSuccess, errors) = await _appUserRepository.ChangePasswordAsync(appUser, formerPassword, newPassword);
            }
            catch (Exception exception)
            {
                return (false, null, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}, İçeriği => {exception.InnerException}");
            }

            if (!isSuccess) return (false, errors, null);
            return (true, null, null);
        }

        public async Task<AppUser?> FindByStringAsync(string Id) => await _appUserRepository.FindByStringAsync(Id);

        public async Task<(bool, string?, AppUser?)> GetUserWithProfileAndAddressAsync(string userName)
        {
            AppUser? appUser;
            try
            {
                appUser = await _appUserRepository.GetUserWithProfileAndAddressAsync(userName);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}. İçeriği => {exception.InnerException}", null);
            }

            if (appUser == null) return (false, "Kullanıcı bulunamadı", null);

            return (true, null, appUser);
        }

        public async Task<(bool, string?, AppUser?)> GetUserWithProfileAsync(string userName)
        {
            AppUser? appUser;
            try
            {
                appUser = await _appUserRepository.GetUserWithProfileAsync(userName);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}. İçeriği => {exception.InnerException}", null);
            }

            if (appUser == null) return (false, "Kullanıcı bulunamadı", null);

            return (true, null, appUser);
        }
    }
}
