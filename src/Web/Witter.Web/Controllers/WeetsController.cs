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

        [Authorize]
        public async Task<IActionResult> Create(WeetCreateModel model, string returnUrl)
        {
            var user = await this._userManager.GetUserAsync(this.User);

            await this._weetsService.Create(model, user);

            return this.Redirect(returnUrl);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Detail(string id)
        {
            if (id == null)
            {
                return this.Redirect("/");
            }

            var weet = this._weetsService.GetByIdToViewModel<FullWeetViewModel>(id);

            if (weet == null)
            {
                return this.Redirect("/");
            }

            return this.View(weet);
        }

        public async Task<IActionResult> Like(string id, string returnUrl)
        {
            var loggedInUser = await this._userManager.GetUserAsync(this.User);

            await this._likeService.Like(loggedInUser.Id, id);

            return this.Redirect(returnUrl);
        }

        public async Task<IActionResult> DisLike(string id, string returnUrl)
        {
            var loggedInUser = await this._userManager.GetUserAsync(this.User);

            await this._likeService.DisLike(loggedInUser.Id, id);

            return this.Redirect(returnUrl);
        }

        // TODO: Redirect to page of action
        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            await this._weetsService.Delete(id);

            return this.Redirect(returnUrl);
        }

        public IActionResult Update(string id)
        {
            return Ok(id);
        }
    }
}
