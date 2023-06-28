﻿using Microsoft.AspNetCore.Identity;
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
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AppUserRepository(MyContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public override async void UpdateAsync(AppUser entity)
        {
            entity.Status = DataStatus.Updated;
            entity.ModifiedDate = DateTime.Now;
            AppUser toBeUpdated = (await FindByStringAsync(entity.Id))!;
            _context.Entry(toBeUpdated).CurrentValues.SetValues(entity);
            SaveAsync();
        }

        public async Task<AppUser?> FindByStringAsync(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }

        public async Task<(bool, IEnumerable<IdentityError>?)> AddUserAsync(AppUser appUser)
        {
            string password = appUser.PasswordHash;//I need pure password for SignIn proccess
            IdentityResult result = await _userManager.CreateAsync(appUser, appUser.PasswordHash);

            if (!result.Succeeded) return (false, result.Errors);

            await _signInManager.PasswordSignInAsync(appUser, password, true, true);
            return (true, null);
        }

        public async Task<(bool, IEnumerable<IdentityError>?)> ChangePasswordAsync(AppUser appUser, string formerPassword, string newPassword)
        {
            IdentityResult result = await _userManager.ChangePasswordAsync(appUser, formerPassword, newPassword);
            if (!result.Succeeded) return (false, result.Errors);

            await _userManager.UpdateSecurityStampAsync(appUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(appUser, newPassword, true, true);

            return (true, null);
        }
    }
}