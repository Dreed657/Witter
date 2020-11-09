using System.ComponentModel.DataAnnotations;

namespace Witter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Witter.Data.Common.Models;

    public class Weet : BaseDeletableModel<string>
    {
        public Weet()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Likes = new HashSet<WeetLikes>();
        }

        public virtual ApplicationUser Author { get; set; }

        public string Content { get; set; }

        public string ImageId { get; set; }

        public Media Image { get; set; }

        public virtual ICollection<WeetLikes> Likes { get; set; }

        public ICollection<WeetTag> Tags { get; set; }
    }
}
