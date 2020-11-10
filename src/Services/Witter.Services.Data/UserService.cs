namespace Witter.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.EntityFrameworkCore;
    using SdvCode.Services.Cloud;
    using Witter.Data.Common.Repositories;
    using Witter.Data.Models;
    using Witter.Services.Data.Contracts;
    using Witter.Services.Mapping;
    using Witter.Web.ViewModels.Profile;
    using Witter.Web.ViewModels.Users;

    public class UserService : IUserService
    {
        private readonly Cloudinary cloudinary;
        private readonly IRepository<ApplicationUser> userRepository;

        public UserService(Cloudinary cloudinary, IRepository<ApplicationUser> repository)
        {
            this.cloudinary = cloudinary;
            this.userRepository = repository;
        }

        public T GetUserByUsername<T>(string username)
        {
            return this.userRepository
                .All()
                .Where(x => x.UserName == username)
                .To<T>()
                .FirstOrDefault();
        }

        public T GetUserById<T>(string id)
        {
            return this.userRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public IEnumerable<string> GetAllUserFollowing(string userId)
        {
            var user = this.userRepository
                .All()
                .Include(x => x.Following)
                .FirstOrDefault(x => x.Id == userId)
                .Following
                .Where(x => x.IsFollowing)
                .Select(x => x.RevicerId);

            return this.userRepository
                .All()
                .Where(x => user.Contains(x.Id))
                .Select(x => x.Id)
                .ToList();
        }

        // TODO: Refactor
        public UserFollowerViewModel GetAllFollowers(string id)
        {
            var user = this.userRepository.All().Where(x => x.Id == id).FirstOrDefault();

            var followerIds = this.userRepository
                .All()
                .Where(x => x.Id == id)
                .Select(x => x.Followers.Select(y => y.SenderId))
                .FirstOrDefault();

            var entities = this.userRepository.All()
                .Where(x => followerIds.Contains(x.Id))
                .To<ShortUserViewModel>()
                .ToList();

            var model = new UserFollowerViewModel()
            {
                Id = id,
                UserName = user.UserName,
                FullName = $"{user.FirstName} {user.LastName}",
                Users = entities,
            };

            return model;
        }

        // TODO: Refactor
        public UserFollowerViewModel GetAllFollowing(string id)
        {
            var user = this.userRepository.All().Where(x => x.Id == id).FirstOrDefault();

            var followerIds = this.userRepository
                .All()
                .Where(x => x.Id == id)
                .Select(x => x.Following.Select(y => y.RevicerId))
                .FirstOrDefault();

            var entities = this.userRepository.All()
                .Where(x => followerIds.Contains(x.Id))
                .To<ShortUserViewModel>()
                .ToList();

            var model = new UserFollowerViewModel()
            {
                Id = id,
                UserName = user.UserName,
                FullName = $"{user.FirstName} {user.LastName}",
                Users = entities,
            };

            return model;
        }

        public ApplicationUser GetUserId(string userId)
        {
            return this.userRepository.All().FirstOrDefaultAsync(x => x.Id == userId).GetAwaiter().GetResult();
        }

        // TODO: Add security
        public async Task<bool> UpdateUser(InputProfileSettingsModel model)
        {
            var entity = this.userRepository.All().Where(x => x.Id == model.Id).FirstOrDefault();

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

            var profileImageName = Guid.NewGuid().ToString();
            var profileImageUrl = await ApplicationCloudinary.UploadImage(this.cloudinary, model.ProfileImageSource, profileImageName);

            if (!string.IsNullOrEmpty(profileImageUrl))
            {
                entity.ProfileImage = new Media()
                {
                    Url = profileImageUrl,
                    Name = profileImageName,
                    Creator = entity,
                };
            }

            var coverImageName = Guid.NewGuid().ToString();
            var coverImageUrl = await ApplicationCloudinary.UploadImage(this.cloudinary, model.CoverImageSource, coverImageName);

            if (!string.IsNullOrEmpty(coverImageUrl))
            {
                entity.CoverImage = new Media()
                {
                    Url = coverImageUrl,
                    Name = coverImageName,
                    Creator = entity,
                };
            }

            this.userRepository.Update(entity);
            await this.userRepository.SaveChangesAsync();

            return true;
        }
    }
}
