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
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IUserService _usersService;

        private readonly IFollowerService _followerService;

        public ProfileController(IUserService userService, IFollowerService followerService, UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
            this._usersService = userService;
            this._followerService = followerService;
        }

        // TODO: Return 404 error page

        [HttpGet("/Profile/{username}")]
        public async Task<IActionResult> Index(string username)
        {
            var profileData = this._usersService.GetProfileByUser(username);

            if (profileData == null)
            {
                return this.RedirectToAction("NotFound", "Home");
                //return this.NotFound($"No matching profile with this username ({username}).");
            }

            //TODO: DELETE!!!
            //await this.userManager.AddToRoleAsync(await this.userManager.GetUserAsync(this.User), GlobalConstants.AdministratorRoleName);
            
            return this.View(profileData);
        }

        public async Task<IActionResult> Follow(string id)
        {
            var loggedInUser = await this._userManager.GetUserAsync(this.User);

            await this._followerService.Follow(loggedInUser.Id, id);

            return this.Redirect($"/");
        }

        public async Task<IActionResult> UnFollow(string id)
        {
            var loggedInUser = await this._userManager.GetUserAsync(this.User);

            await this._followerService.UnFollow(loggedInUser.Id, id);

            return this.Redirect($"/");
        }
    }
}
