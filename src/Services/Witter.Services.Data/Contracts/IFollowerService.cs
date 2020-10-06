using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Witter.Services.Data.Contracts
{
    public interface IFollowerService
    {
        int FollowersCount(string userId);

        int FollowingCount(string userId);

        Task Follow(string parentId, string followerId);

        Task UnFollow(string parentId, string followerId);

        bool IsFollowing(string parentId, string followerId);
    }
}
