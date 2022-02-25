using Assignment2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Controllers
{

    public class UsersController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager; 
        public UsersController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Get Login UserId 
            var LoginUser = await _userManager.GetUserAsync(User);
            //IEnumerable<IdentityUser> users = new IEnumerable<IdentityUser>();
            var users = await _userManager.Users.Where(t => t.Id != LoginUser.Id).ToListAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string UserId)
        {
            var users = await _userManager.FindByIdAsync(UserId);
            if (users != null)
            {
                users.LockoutEnd = null;
                _db.Update(users);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
