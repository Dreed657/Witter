using System;
using System.ComponentModel.DataAnnotations;
using Witter.Data.Common.Models;

namespace Witter.Data.Models
{
    public class WeetLikes : BaseModel<Guid>
    {
        [Required]
        public string ParentId { get; set; }

        [Required]
        public string WeetId { get; set; }

        public bool IsLiked { get; set; }
    }
}
