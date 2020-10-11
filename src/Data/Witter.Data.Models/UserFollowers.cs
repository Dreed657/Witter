using System;
using System.ComponentModel.DataAnnotations;
using Witter.Data.Common.Models;

namespace Witter.Data.Models
{
    public class UserFollowers : BaseModel<string>
    {
        public UserFollowers()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string FollowerId { get; set; }

        public virtual ApplicationUser Follower { get; set; }

        [Required]
        public string FollowingId { get; set; }

        public virtual ApplicationUser Following { get; set; }

        public bool IsFollowing { get; set; }
    }
}
