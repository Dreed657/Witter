namespace Witter.Web.Controllers
{
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
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public IActionResult NotFound()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            return this.View();
        }
    }
}
