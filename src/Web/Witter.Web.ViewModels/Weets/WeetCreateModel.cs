using Microsoft.AspNetCore.Http;

namespace Witter.Web.ViewModels.Weets
{
    public class WeetCreateModel
    {
        public string Content { get; set; }

        public string Tags { get; set; }

        public IFormFile Image { get; set; }
    }
}
