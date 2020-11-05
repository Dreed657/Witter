using System.Collections.Generic;

namespace Witter.Web.ViewModels.Users
{
    public class UserFollowerViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public ICollection<ShortUserViewModel> Users { get; set; }
    }
}
