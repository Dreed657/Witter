using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Witter.Data.Common.Repositories;
using Witter.Data.Models;
using Witter.Data.Models.Enums;
using Witter.Services.Data.Contracts;

namespace Witter.Services.Data
{
    public class FollowerService : IFollowerService
    {
        private readonly IRepository<UserFollowers> followerRepository;

        private readonly IUserService userService;

        private readonly INotificationsService notificationsService;

        public FollowerService(IRepository<UserFollowers> repository, IUserService userService, INotificationsService notificationsService)
        {
            this.followerRepository = repository;
            this.userService = userService;
            this.notificationsService = notificationsService;
        }

        // TODO: Properties are mapping in reverse 
        // Data models for userFollowers table should change.

        public async Task Follow(string parentId, string followerId)
        {
            var entity = this.followerRepository
                .All()
                .FirstOrDefault(x => x.FollowingId == parentId && x.FollowerId == followerId);
            var parent = this.userService.GetUserById(parentId);
            var following = this.userService.GetUserById(followerId);

            if (entity == null)
            {
                var insertEntity = new UserFollowers()
                {
                    Follower = following,
                    Following = parent,
                    IsFollowing = true,
                };

                await this.followerRepository.AddAsync(insertEntity);
            }
            else
            {
                entity.IsFollowing = true;
            }

            await this.notificationsService.AddNotificationAsync(parent, following, NotificationType.Follow);
            await this.followerRepository.SaveChangesAsync();
        }

        public async Task UnFollow(string parentId, string followerId)
        {
            var entity = this.followerRepository
                .All()
                .FirstOrDefaultAsync(x => x.FollowingId == parentId && x.FollowerId == followerId)
                .GetAwaiter()
                .GetResult();

            if (entity.IsFollowing)
            {
                entity.IsFollowing = false;
                await this.followerRepository.SaveChangesAsync();
            }
        }

        public bool IsFollowing(string parentId, string followerId)
        {
            var entity = this.followerRepository
                .All()
                .FirstOrDefaultAsync(x => x.FollowingId == parentId && x.FollowerId == followerId)
                .GetAwaiter()
                .GetResult();

            return entity != null && entity.IsFollowing;
        }
    }
}
