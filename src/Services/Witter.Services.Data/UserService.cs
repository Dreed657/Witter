using System;
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

        public ProfileViewModel GetProfileByUser(string username)
        {
            return this._userRepository
                .All()
                .To<ProfileViewModel>()
                .ToList()
                .FirstOrDefault(x => string.Compare(x.UserName, username, StringComparison.OrdinalIgnoreCase) == 0);
        }
    }
}
