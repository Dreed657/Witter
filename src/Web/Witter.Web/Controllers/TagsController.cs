namespace Witter.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Witter.Services.Contracts;
    using Witter.Web.ViewModels.Tags;
    using Witter.Web.ViewModels.Weets;

    public class TagsController : Controller
    {
        private readonly IWeetsService weetsService;

        public TagsController(IWeetsService weetsService)
        {
            this.weetsService = weetsService;
        }

        [HttpGet("{name}")]
        public IActionResult Index(string name)
        {
            var entities = this.weetsService.GetAllByTagId<FullWeetViewModel>(name);
            var model = new TagsIndexViewModel() { TagName = name, Weets = entities.ToList() };
            return this.View(model);
        }
    }
}
