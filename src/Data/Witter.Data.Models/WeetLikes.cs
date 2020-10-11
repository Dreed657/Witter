using System;
using System.ComponentModel.DataAnnotations;
using Witter.Data.Common.Models;

namespace Witter.Data.Models
{
    public class WeetLikes : BaseModel<string>
    {
        public WeetLikes()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string WeetId { get; set; }

        public virtual Weet Weet { get; set; }

        public bool IsLiked { get; set; }
    }
}
