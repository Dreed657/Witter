namespace Witter.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Witter.Data.Common.Repositories;
    using Witter.Data.Models;
    using Witter.Services.Data.Contracts;
    using Witter.Services.Mapping;
    using Witter.Web.ViewModels.Profile;
    using Witter.Web.ViewModels.Users;

    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> _userRepository;

        public UserService(IRepository<ApplicationUser> repository)
        {
            this._userRepository = repository;
        }

        public T GetUserByUsername<T>(string username)
        {
            return this._userRepository
                .All()
                .Where(x => x.UserName == username)
                .To<T>()
                .FirstOrDefault();
        }

        public T GetUserById<T>(string id)
        {
            return this._userRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public IEnumerable<string> GetAllUserFollowing(string userId)
        {
            var user = this._userRepository
                .All()
                .Include(x => x.Following)
                .FirstOrDefault(x => x.Id == userId)
                .Following
                .Where(x => x.IsFollowing)
                .Select(x => x.RevicerId);

            return this._userRepository
                .All()
                .Where(x => user.Contains(x.Id))
                .Select(x => x.Id)
                .ToList();
        }

        // TODO: Refactor
        public UserFollowerViewModel GetAllFollowers(string Id)
        {
            var user = this._userRepository.All().Where(x => x.Id == Id).FirstOrDefault();

            var followerIds = this._userRepository
                .All()
                .Where(x => x.Id == Id)
                .Select(x => x.Followers.Select(y => y.SenderId))
                .FirstOrDefault();

            var entities = this._userRepository.All()
                .Where(x => followerIds.Contains(x.Id))
                .To<ShortUserViewModel>()
                .ToList();

            var model = new UserFollowerViewModel()
            {
                Id = Id,
                UserName = user.UserName,
                FullName = $"{user.FirstName} {user.LastName}",
                Users = entities,
            };

            return model;
        }

        // TODO: Refactor
        public UserFollowerViewModel GetAllFollowing(string Id)
        {
            var user = this._userRepository.All().Where(x => x.Id == Id).FirstOrDefault();

            var followerIds = this._userRepository
                .All()
                .Where(x => x.Id == Id)
                .Select(x => x.Following.Select(y => y.RevicerId))
                .FirstOrDefault();

            var entities = this._userRepository.All()
                .Where(x => followerIds.Contains(x.Id))
                .To<ShortUserViewModel>()
                .ToList();

            var model = new UserFollowerViewModel()
            {
                Id = Id,
                UserName = user.UserName,
                FullName = $"{user.FirstName} {user.LastName}",
                Users = entities,
            };

            return model;
        }

        public ApplicationUser GetUserId(string userId)
        {
            return this._userRepository.All().FirstOrDefaultAsync(x => x.Id == userId).GetAwaiter().GetResult();
        }

        // TODO: Add security
        public async Task<bool> UpdateUser(InputProfileSettingsModel model)
        {
            var entity = this._userRepository.All().Where(x => x.Id == model.Id).FirstOrDefault();

            if (entity == null)
            {
                return false;
            }

            entity.UserName = model.UserName;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.AboutMe = model.AboutMe;
            entity.BirthDate = model.BirthDate;
            entity.PhoneNumber = model.PhoneNumber;

            this._userRepository.Update(entity);
            await this._userRepository.SaveChangesAsync();

            return true;
        }
    }
}
