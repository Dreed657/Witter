namespace Witter.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Witter.Data.Models;
    using Witter.Services.Data.Contracts;
    using Witter.Web.ViewModels.Profile;

    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService usersService;

        private readonly IFollowerService followerService;

        public ProfileController(IUserService userService, IFollowerService followerService, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.usersService = userService;
            this.followerService = followerService;
        }

        // TODO: Return 404 error page
        [HttpGet("/Profile/{username}")]
        public IActionResult Index(string username)
        {
            var profileData = this.usersService.GetUserByUsername<ProfileViewModel>(username);

            if (profileData == null)
            {
                return this.RedirectToAction("NotFound", "Home");
            }

            return this.View(profileData);
        }

        public async Task<IActionResult> Follow(string id, string returnUrl)
        {
            var loggedInUser = await this.userManager.GetUserAsync(this.User);

            await this.followerService.Follow(loggedInUser.Id, id);

            return this.Redirect(returnUrl);
        }

        public async Task<IActionResult> UnFollow(string id, string returnUrl)
        {
            var loggedInUser = await this.userManager.GetUserAsync(this.User);

            await this.followerService.UnFollow(loggedInUser.Id, id);

            return this.Redirect(returnUrl);
        }

        [Route("/Profile/{id}/Followers")]
        public IActionResult Followers(string id)
        {
            var model = this.usersService.GetAllFollowers(id);
            return this.View(model);
        }

        [Route("/Profile/{id}/Followings")]
        public IActionResult Followings(string id)
        {
            var model = this.usersService.GetAllFollowing(id);
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Settings()
        {
            var userId = this.userManager.GetUserId(this.User);
            var model = this.usersService.GetUserById<InputProfileSettingsModel>(userId);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(InputProfileSettingsModel model)
        {
            await this.usersService.UpdateUser(model);

            return this.RedirectToAction(nameof(this.Index), new { username = model.UserName });
        }
    }
}
