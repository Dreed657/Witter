using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Witter.Data.Models;
using Witter.Services.Mapping;
using Witter.Web.ViewModels.Weets;

namespace Witter.Web.ViewModels.Profile
{
    public class ProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public int Followers { get; set; }

        [NotMapped]
        public int Following { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<FullWeetViewModel> Weets { get; set; }

        public string Tag { get; set; }
    }
}
