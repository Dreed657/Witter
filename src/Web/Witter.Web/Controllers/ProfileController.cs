using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Witter.Common;
using Witter.Data.Models;
using Witter.Services.Data.Contracts;

namespace Witter.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService usersService;

        public ProfileController(IUserService service, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.usersService = service;
        }

        // TODO: Return 404 error page

        [Authorize]
        [HttpGet("/Profile/{username}")]
        public async Task<IActionResult> Index(string username)
        {
            var profileData = this.usersService.GetProfileByUser(username);

            if (profileData == null)
            {
                return this.RedirectToAction("NotFound", "Home");
                //return this.NotFound($"No matching profile with this username ({username}).");
            }

            //await this.userManager.AddToRoleAsync(await this.userManager.GetUserAsync(this.User), GlobalConstants.AdministratorRoleName);
            
            return this.View(profileData);
        }
    }
}
