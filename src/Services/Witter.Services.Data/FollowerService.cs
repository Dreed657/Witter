using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Witter.Data.Common.Repositories;
using Witter.Data.Models;
using Witter.Services.Data.Contracts;

namespace Witter.Services.Data
{
    public class FollowerService : IFollowerService
    {
        private readonly IRepository<UserFollowers> followerRepository;

        public FollowerService(IRepository<UserFollowers> repository)
        {
            this.followerRepository = repository;
        }

        public async Task Follow(string parentId, string followerId)
        {
            var entity = this.followerRepository
                .All()
                .FirstOrDefault(x => x.FollowingId == parentId && x.FollowerId == followerId);

            if (entity == null)
            {
                var insertEntity = new UserFollowers()
                {
                    FollowingId = parentId,
                    FollowerId = followerId,
                    IsFollowing = true,
                };

                await this.followerRepository.AddAsync(insertEntity);
            }
            else
            {
                entity.IsFollowing = true;
            }

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
