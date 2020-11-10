namespace Witter.Web.ViewModels.Tags
{
    using System.Collections.Generic;

    using Witter.Web.ViewModels.Weets;

    public class TagsIndexViewModel
    {
        public string TagName { get; set; }

        public ICollection<FullWeetViewModel> Weets { get; set; }
    }
}
