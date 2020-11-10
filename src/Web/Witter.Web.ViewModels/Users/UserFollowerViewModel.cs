namespace Witter.Web.ViewModels.Users
{
    using System.Collections.Generic;

    public class UserFollowerViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public ICollection<ShortUserViewModel> Users { get; set; }
    }
}
