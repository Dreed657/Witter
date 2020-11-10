namespace Witter.Web.ViewModels.Weets
{
    using System;

    using Witter.Data.Models;
    using Witter.Services.Mapping;

    public class DetailedWeetViewModel : IMapFrom<Weet>
    {
        public string Id { get; set; }

        public ApplicationUser Author { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
