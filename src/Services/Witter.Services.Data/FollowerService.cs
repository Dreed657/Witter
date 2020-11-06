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
        private readonly IDeletableEntityRepository<UserFollowers> followerRepository;

        private readonly IUserService userService;

        private readonly INotificationsService notificationsService;

        public FollowerService(IDeletableEntityRepository<UserFollowers> repository, IUserService userService, INotificationsService notificationsService)
        {
            this.followerRepository = repository;
            this.userService = userService;
            this.notificationsService = notificationsService;
        }

        // TODO: Properties are mapping in reverse 
        // Data models for userFollowers table should change.

        public async Task Follow(string senderId, string reciverId)
        {
            var entity = this.followerRepository
                .AllWithDeleted()
                .FirstOrDefault(x => x.RevicerId == reciverId && x.SenderId == senderId);

            var sender = this.userService.GetUserId(senderId);
            var revicer = this.userService.GetUserId(reciverId);

            if (entity == null || !entity.IsDeleted)
            {
                var insertEntity = new UserFollowers()
                {
                    Sender = sender,
                    Revicer = revicer,
                    IsFollowing = true,
                };
                await this.followerRepository.AddAsync(insertEntity);
            }
            else
            {
                entity.IsFollowing = true;
                this.followerRepository.Undelete(entity);
            }

            await this.notificationsService.AddNotificationAsync(sender, revicer, NotificationType.Follow);
            await this.followerRepository.SaveChangesAsync();
        }

        public async Task UnFollow(string senderId, string reciverId)
        {
            var entity = this.followerRepository
                .All()
                .FirstOrDefaultAsync(x => x.RevicerId == reciverId && x.SenderId == senderId)
                .GetAwaiter()
                .GetResult();

            if (entity.IsFollowing)
            {
                entity.IsFollowing = false;
                this.followerRepository.Delete(entity);
                await this.followerRepository.SaveChangesAsync();
            }
        }

        public bool IsFollowing(string senderId, string reciverId)
        {
            var entity = this.followerRepository
                .All()
                .FirstOrDefaultAsync(x => x.RevicerId == reciverId && x.SenderId == senderId)
                .GetAwaiter()
                .GetResult();

            return entity != null && entity.IsFollowing;
        }
    }
}
