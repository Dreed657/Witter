using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Witter.Services.Contracts;
using Witter.Services.Data.Contracts;
using Witter.Web.ViewModels.Tags;
using Witter.Web.ViewModels.Weets;

namespace Witter.Web.Controllers
{
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
