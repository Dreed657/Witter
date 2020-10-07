﻿using System;
using System.ComponentModel.DataAnnotations;
using Witter.Data.Common.Models;

namespace Witter.Data.Models
{
    public class UserFollowers : BaseModel<Guid>
    {
        [Required]
        public string ParentId { get; set; }

        [Required]
        public string FollowerId { get; set; }

        public bool IsFollowing { get; set; }
    }
}
