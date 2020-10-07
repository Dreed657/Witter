using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Witter.Data.Models;
using Witter.Services.Contracts;
using Witter.Services.Data.Contracts;
using Witter.Web.ViewModels.Weets;

namespace Witter.Web.Controllers
{
    public class WeetsController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IWeetsService _weetsService;

        private readonly ILikeService _likeService;

        public WeetsController(IWeetsService weetService, ILikeService likeService, UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
            this._weetsService = weetService;
            this._likeService = likeService;
        }

        [Route("Feed")]
        public IActionResult Index()
        {
            var weets = this._weetsService.Feed();

            return this.View(weets);
        }

        // TODO: Redirect to page of action
        [Authorize]
        public async Task<IActionResult> Create(WeetCreateModel model)
        {
            var user = await this._userManager.GetUserAsync(this.User);

            await this._weetsService.Create(model, user);

            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult Detail(string id)
        {
            if (id == null)
            {
                return this.RedirectToAction("NotFound", "Home");
            }

            var weet = this._weetsService.Get(id);

            if (weet == null)
            {
                return this.RedirectToAction("NotFound", "Home");
            }

            return this.View(weet);
        }

        public async Task<IActionResult> Like(string id)
        {
            var loggedInUser = await this._userManager.GetUserAsync(this.User);

            await this._likeService.Like(loggedInUser.Id, id);

            return this.Redirect("/");
        }

        public async Task<IActionResult> DisLike(string id)
        {
            var loggedInUser = await this._userManager.GetUserAsync(this.User);

            await this._likeService.DisLike(loggedInUser.Id, id);

            return this.Redirect("/");
        }

        // TODO: Redirect to page of action
        public async Task<IActionResult> Delete(string id)
        {
            await this._weetsService.Delete(id);

            return this.Redirect("/");
        }

        public IActionResult Update(string id)
        {
            return Ok(id);
        }
    }
}
