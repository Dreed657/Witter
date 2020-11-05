using Witter.Data.Models;
using Witter.Services.Mapping;

namespace Witter.Web.ViewModels.Users
{
    public class ShortUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public string UserName { get; set; }

        public string AboutMe { get; set; }
    }
}
