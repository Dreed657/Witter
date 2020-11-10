namespace Witter.Web.ViewModels.Weets
{
    using Microsoft.AspNetCore.Http;

    public class WeetCreateModel
    {
        public string Content { get; set; }

        public string Tags { get; set; }

        public IFormFile Image { get; set; }
    }
}
