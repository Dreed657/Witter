namespace Witter.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Witter.Common;
    using Witter.Data.Models;
    using Witter.Services.Data.Contracts;

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
            }

            return this.View(profileData);
        }

        public async Task<IActionResult> Follow(string id, string returnUrl)
        {
            var loggedInUser = await this._userManager.GetUserAsync(this.User);

            await this._followerService.Follow(loggedInUser.Id, id);

            return this.Redirect(returnUrl);
        }

        public async Task<IActionResult> UnFollow(string id, string returnUrl)
        {
            var loggedInUser = await this._userManager.GetUserAsync(this.User);

            await this._followerService.UnFollow(loggedInUser.Id, id);

            return this.Redirect(returnUrl);
        }

        [Route("/Profile/{id}/Followers")]
        public IActionResult Followers(string id)
        {
            var model = this._usersService.GetAllFollowers(id);
            return this.View(model);
        }

        [Route("/Profile/{id}/Followings")]
        public IActionResult Followings(string id)
        {
            var model = this._usersService.GetAllFollowing(id);
            return this.View(model);
        }
    }
}
