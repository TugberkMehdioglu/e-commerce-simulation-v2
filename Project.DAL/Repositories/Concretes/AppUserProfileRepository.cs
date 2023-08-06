using Project.DAL.ContextClasses;
using Project.DAL.Repositories.Abstarcts;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Concretes
{
    public class AppUserProfileRepository : BaseRepository<AppUserProfile>, IAppUserProfileRepository
    {
        public AppUserProfileRepository(MyContext context) : base(context)
        {
        }

        public async Task<AppUserProfile?> FindByStringAsync(string Id)
        {
            return await _context.AppUserProfiles!.FindAsync(Id);
        }

        public override async Task UpdateAsync(AppUserProfile entity)
        {
            entity.Status = DataStatus.Updated;
            entity.ModifiedDate = DateTime.Now;
            AppUserProfile toBeUpdated = (await FindByStringAsync(entity.Id))!;
            _context.Entry(toBeUpdated).CurrentValues.SetValues(entity);
            await SaveAsync();
        }
    }
}
