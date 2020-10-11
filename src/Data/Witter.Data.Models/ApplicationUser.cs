// ReSharper disable VirtualMemberCallInConstructor
namespace Witter.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Witter.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Weets = new HashSet<Weet>();
            this.Followers = new HashSet<UserFollowers>();
            this.Following = new HashSet<UserFollowers>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Tag { get; set; }

        public string AboutMe { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Weet> Weets { get; set; }

        public virtual ICollection<UserFollowers> Followers { get; set; }

        public virtual ICollection<UserFollowers> Following { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
