using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Witter.Data.Models;
using Witter.Services.Contracts;
using Witter.Web.ViewModels.Weets;

namespace Witter.Web.Controllers
{
    public class WeetsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IWeetsService weetsService;

        public WeetsController(IWeetsService service, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.weetsService = service;
        }

        [Route("Feed")]
        public IActionResult Index()
        {
            var weets = this.weetsService.Feed();

            return this.View(weets);
        }

        [Authorize]
        public async Task<IActionResult> Create(WeetCreateModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.weetsService.Create(model, user);

            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult Detail(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weet = this.weetsService.Get(id);

            if (weet == null)
            {
                return this.NotFound();
            }

            this.ViewBag.Weet = weet;

            return this.View();
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.weetsService.Delete(id);

            return this.RedirectToAction(nameof(this.Index));
        }

        /// <summary>
        ///   Change the redirect to something more flexible.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Like(string id)
        {
            await this.weetsService.Like(id);

            return this.Redirect("/Feed#" + id);
        }

        public IActionResult Update(string id)
        {
            return Ok(id);
        }
    }
}
