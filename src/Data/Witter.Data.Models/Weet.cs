using System.ComponentModel.DataAnnotations;

namespace Witter.Data.Models
{
    using System;

    using Witter.Data.Common.Models;

    public class Weet : BaseDeletableModel<Guid>
    {
        public ApplicationUser Author { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }
    }
}
