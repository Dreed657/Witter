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
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Tag { get; set; }
        
        // TODO: Add About me string property;

        /// <summary>
        ///   TODO: Add Country and tables for them
        /// 
        ///   https://camo.githubusercontent.com/4497fd270be74438353a9ae531323308f8a6e9a1/68747470733a2f2f7265732e636c6f7564696e6172792e636f6d2f64766c7731656870612f696d6167652f75706c6f61642f76313539323433303033382f4469616772616d5f70736571676b2e706e67
        /// </summary>

        public ICollection<Weet> Weets { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
