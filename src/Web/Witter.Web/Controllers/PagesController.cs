using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Witter.Data.Models;
using Witter.Services.Contracts;

namespace Witter.Web.Controllers
{
    public class PagesController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IWeetsService _weetsService;

        public PagesController(IWeetsService weetService, UserManager<ApplicationUser> userManager)
        {
            this._weetsService = weetService;
            this._userManager = userManager;
        }

        [Authorize]
        [Route("Feed")]
        public IActionResult Feed()
        {
            var user = this._userManager.GetUserAsync(this.User).GetAwaiter().GetResult();

            var weets = this._weetsService.Feed(user.Id);

            return this.View(weets);
        }

        [Route("Explore")]
        public IActionResult Explore()
        {
            var weets = this._weetsService.Explore();

            return this.View(weets);
        }

        public IActionResult Notifications()
        {
            return View();
        }
    }
}
