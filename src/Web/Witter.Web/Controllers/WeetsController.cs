namespace Witter.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Witter.Data.Models;
    using Witter.Services.Contracts;
    using Witter.Services.Data.Contracts;
    using Witter.Web.ViewModels.Weets;

    public class WeetsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IWeetsService weetsService;

        private readonly ILikeService likeService;

        public WeetsController(IWeetsService weetService, ILikeService likeService, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.weetsService = weetService;
            this.likeService = likeService;
        }

        [Authorize]
        public async Task<IActionResult> Create(WeetCreateModel model, string returnUrl)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.weetsService.Create(model, user);

            return this.Redirect(returnUrl);
        }

        [HttpGet("Details")]
        public IActionResult Detail(string id)
        {
            if (id == null)
            {
                return this.Redirect("/");
            }

            var weet = this.weetsService.GetByIdToViewModel<FullWeetViewModel>(id);

            if (weet == null)
            {
                return this.Redirect("/");
            }

            return this.View(weet);
        }

        public async Task<IActionResult> Like(string id, string returnUrl)
        {
            var loggedInUser = await this.userManager.GetUserAsync(this.User);

            await this.likeService.Like(loggedInUser.Id, id);

            return this.Redirect(returnUrl);
        }

        public async Task<IActionResult> DisLike(string id, string returnUrl)
        {
            var loggedInUser = await this.userManager.GetUserAsync(this.User);

            await this.likeService.DisLike(loggedInUser.Id, id);

            return this.Redirect(returnUrl);
        }

        // TODO: Redirect to page of action
        public async Task<IActionResult> Delete(string id, string returnUrl)
        {
            await this.weetsService.Delete(id);

            return this.Redirect(returnUrl);
        }

        public IActionResult Update(string id)
        {
            return this.Ok(id);
        }
    }
}
