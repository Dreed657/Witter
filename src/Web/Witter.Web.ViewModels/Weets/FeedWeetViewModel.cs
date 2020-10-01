using System;
using System.Collections.Generic;
using System.Text;
using Witter.Data.Models;
using Witter.Services.Mapping;

namespace Witter.Web.ViewModels.Weets
{
    public class FeedWeetViewModel : IMapFrom<Weet>
    {
        public Guid Id { get; set; }

        public ApplicationUser Author { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
