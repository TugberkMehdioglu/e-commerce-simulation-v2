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
    public class AppUserProfileManager : BaseManager<AppUserProfile>, IAppUserProfileManager
    {
        private readonly IAppUserProfileRepository _appUserProfileRepository;
        public AppUserProfileManager(IRepository<AppUserProfile> repository, IAppUserProfileRepository appUserProfileRepository) : base(repository)
        {
            _appUserProfileRepository = appUserProfileRepository;
        }

        public async Task<AppUserProfile?> FindByStringAsync(string Id) => await _appUserProfileRepository.FindByStringAsync(Id);

        public override async Task<(bool, string?)> UpdateAsync(AppUserProfile entity)
        {
            if (entity == null || entity.Status == DataStatus.Deleted) return (false, "Lütfen zorunlu alanları doldurunuz");

            try
            {
                await _appUserProfileRepository.UpdateAsync(entity);
            }
            catch (Exception exception)
            {
                return (false, $"Veritabanı işlemi sırasında hata oluştu, alınan hata => {exception.Message}. İçeriği => {exception.InnerException}");
            }
            return (true, null);
        }
    }
}
