using System;
using System.Collections.Generic;
using Witter.Data.Models;
using Witter.Services.Mapping;
using Witter.Web.ViewModels.Weets;

namespace Witter.Web.ViewModels.Profile
{
    public class ProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<FullWeetViewModel> Weets { get; set; }

        public string Tag { get; set; }
    }
}
