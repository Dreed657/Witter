namespace Witter.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Witter.Data.Models;
    using Witter.Web.ViewModels.Profile;
    using Witter.Web.ViewModels.Users;

    public interface IUserService
    {
        T GetUserByUsername<T>(string username);

        T GetUserById<T>(string username);

        Task<bool> UpdateUser(InputProfileSettingsModel model);

        IEnumerable<string> GetAllUserFollowing(string userId);

        ApplicationUser GetUserId(string userId);

        UserFollowerViewModel GetAllFollowers(string id);

        UserFollowerViewModel GetAllFollowing(string id);
    }
}
