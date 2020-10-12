using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Xsl;
using Witter.Data.Models;
using Witter.Services.Contracts;
using Witter.Services.Data.Contracts;

namespace Witter.Web.Controllers
{
    public class PagesController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWeetsService _weetsService;
        private readonly INotificationsService notificationsService;

        public PagesController(IWeetsService weetService, INotificationsService notificationsService, UserManager<ApplicationUser> userManager)
        {
            this._weetsService = weetService;
            this.notificationsService = notificationsService;
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

        [Authorize]
        public IActionResult Notifications()
        {
            var user = this._userManager.GetUserAsync(this.User).GetAwaiter().GetResult();
            var entities = this.notificationsService.GetAllNotificationByUserId(user.Id);

            return View(entities);
        }
    }
}
