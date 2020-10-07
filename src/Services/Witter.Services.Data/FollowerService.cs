using System;
using System.Linq;
using System.Threading.Tasks;
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

        public int GetFollowersCount(string userId)
        {
            return this.followerRepository
                .All()
                .Where(x => x.IsFollowing)
                .Count(x => x.FollowerId == userId);
        }

        public int GetFollowingCount(string userId)
        {
            return this.followerRepository
                .All()
                .Where(x => x.IsFollowing)
                .Count(x => x.ParentId == userId);
        }

        public async Task Follow(string parentId, string followerId)
        {
            var alreadyInDb = this.followerRepository
                .All()
                .FirstOrDefault(x => x.ParentId == parentId && x.FollowerId == followerId);

            if (alreadyInDb == null)
            {
                var entity = new UserFollowers()
                {
                    ParentId = parentId,
                    FollowerId = followerId,
                    IsFollowing = true,
                };

                await this.followerRepository.AddAsync(entity);
            }
            else
            {
                alreadyInDb.IsFollowing = true;
            }

            await this.followerRepository.SaveChangesAsync();
        }

        public async Task UnFollow(string parentId, string followerId)
        {
            var entity = this.followerRepository.All().FirstOrDefault(x => x.ParentId == parentId && x.FollowerId == followerId);

            if (entity.IsFollowing)
            {
                entity.IsFollowing = false;
                await this.followerRepository.SaveChangesAsync();
            }
        }

        public bool IsFollowing(string parentId, string followerId)
        {
            var entity = this.followerRepository.All()
                .FirstOrDefault(x => x.ParentId == parentId && x.FollowerId == followerId);

            return entity != null && entity.IsFollowing;
        }
    }
}
