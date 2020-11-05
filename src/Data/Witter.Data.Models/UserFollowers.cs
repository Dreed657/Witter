using System;
using System.ComponentModel.DataAnnotations;
using Witter.Data.Common.Models;

namespace Witter.Data.Models
{
    public class UserFollowers : IDeletableEntity
    {
        public UserFollowers()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        [Required]
        public string RevicerId { get; set; }

        public virtual ApplicationUser Revicer { get; set; }

        public bool IsFollowing { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
