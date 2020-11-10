using System.Threading.Tasks;

namespace Witter.Services.Data.Contracts
{
    public interface IFollowerService
    {
        Task Follow(string senderId, string reviverId);

        Task UnFollow(string senderId, string reviverId);

        bool IsFollowing(string senderId, string reviverId);
    }
}
