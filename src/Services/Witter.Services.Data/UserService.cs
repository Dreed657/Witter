using System;
using System.Linq;
using System.Runtime.InteropServices;
using Witter.Data.Common.Repositories;
using Witter.Data.Models;
using Witter.Services.Data.Contracts;
using Witter.Services.Mapping;
using Witter.Web.ViewModels.Profile;

namespace Witter.Services.Data
{
    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> _userRepository;

        private readonly IFollowerService _followerService;

        public UserService(IRepository<ApplicationUser> repository, IFollowerService followerService)
        {
            this._userRepository = repository;
            this._followerService = followerService;
        }

        public ProfileViewModel GetProfileByUser(string username)
        {
            var entity = this._userRepository
                .All()
                .To<ProfileViewModel>()
                .ToList()
                .FirstOrDefault(x => string.Compare(x.UserName, username, StringComparison.OrdinalIgnoreCase) == 0);

            if (entity == null)
            {
                return null;
            }

            return entity;
        }
    }
}
