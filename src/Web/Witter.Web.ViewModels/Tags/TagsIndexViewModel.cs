using System.Collections.Generic;
using Witter.Web.ViewModels.Weets;

namespace Witter.Web.ViewModels.Tags
{
    public class TagsIndexViewModel
    {
        public string TagName { get; set; }

        public ICollection<FullWeetViewModel> Weets { get; set; }
    }
}
