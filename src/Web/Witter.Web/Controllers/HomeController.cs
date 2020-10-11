namespace Witter.Web.Controllers
{
    using System.Diagnostics;

    using Witter.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.RedirectPermanent("/Explore");
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [HttpGet("404")]
        public IActionResult NotFound()
        {
            return this.View();
        }
    }
}
