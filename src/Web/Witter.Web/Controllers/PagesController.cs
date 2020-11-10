namespace Witter.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Witter.Data.Models;
    using Witter.Services.Contracts;
    using Witter.Services.Data.Contracts;

    public class PagesController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWeetsService weetsService;
        private readonly INotificationsService notificationsService;

        public PagesController(IWeetsService weetService, INotificationsService notificationsService, UserManager<ApplicationUser> userManager)
        {
            this.weetsService = weetService;
            this.notificationsService = notificationsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpGet("Feed")]
        public IActionResult Feed()
        {
            var user = this.userManager.GetUserAsync(this.User).GetAwaiter().GetResult();
            var weets = this.weetsService.Feed(user.Id);

            return this.View(weets);
        }

        [HttpGet("Explore")]
        public IActionResult Explore()
        {
            var weets = this.weetsService.Explore();

            return this.View(weets);
        }

        [Authorize]
        [HttpGet("Notifications")]
        public IActionResult Notifications()
        {
            var user = this.userManager.GetUserAsync(this.User).GetAwaiter().GetResult();
            var entities = this.notificationsService.GetAllNotificationByUserId(user.Id);

            return this.View(entities);
        }
    }
}
