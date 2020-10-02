using Witter.Data.Models;
using Witter.Web.ViewModels.Profile;

namespace Witter.Services.Data.Contracts
{
    public interface IUserService
    {
        ProfileViewModel GetProfileByUser(ApplicationUser user);
    }
}
