using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Assignment2.Utility.enumManager;

namespace Assignment2.Data
{
    public static class UserDbIntializer
    {
        public static void initializer(ApplicationDbContext _db, UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch { }


            //If Admin in not exists
            if (!_db.Roles.Any(r => r.Name == eRoles.Admin.ToString()))
            {

                _roleManager.CreateAsync(new IdentityRole { Name = eRoles.Admin.ToString() }).GetAwaiter().GetResult();
            }

            //If customer
            if (!_db.Roles.Any(r => r.Name == eRoles.Student.ToString()))
            {
                _roleManager.CreateAsync(new IdentityRole { Name = eRoles.Student.ToString() }).GetAwaiter().GetResult();
            }

            var user = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                LockoutEnabled =false
            };
            _userManager.CreateAsync(user, "Admin@123").GetAwaiter().GetResult();

            //var user = _db.ApplicationUser.Where(u => u.Email == "admin@gmail.com").FirstOrDefault();
            ////var user = _unitOfWork.ApplicationUserRepository.GetFirstOrDefault(x => x.Email == "admin@gmail.com").GetAwaiter().GetResult();
            //if (user != null)
            //{
            //    _db.Entry(user).State = EntityState.Detached;
            //}
            _userManager.AddToRoleAsync(user, eRoles.Admin.ToString()).GetAwaiter().GetResult();

        }
    }
}
