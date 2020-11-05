using System.Collections.Generic;
using Witter.Data.Models;
using Witter.Web.ViewModels.Profile;
using Witter.Web.ViewModels.Users;

namespace Witter.Services.Data.Contracts
{
    public interface IUserService
    {
        ProfileViewModel GetProfileByUser(string username);

        IEnumerable<string> GetAllUserFollowing(string userId);

        ApplicationUser GetUserById(string userId);

        UserFollowerViewModel GetAllFollowers(string Id);

        UserFollowerViewModel GetAllFollowing(string Id);
    }
}
