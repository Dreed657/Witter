using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Witter.Data.Common.Repositories;
using Witter.Data.Models;
using Witter.Services.Data.Contracts;
using Witter.Services.Mapping;
using Witter.Web.ViewModels.Profile;

namespace Witter.Services.Data
{
    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> _userRepository;

        public UserService(IDeletableEntityRepository<ApplicationUser> repository)
        {
            this._userRepository = repository;
        }

        public ProfileViewModel GetProfileByUser(ApplicationUser user)
        {
            return this._userRepository.All().Where(x => x == user).To<ProfileViewModel>().First();
        }
    }
}
