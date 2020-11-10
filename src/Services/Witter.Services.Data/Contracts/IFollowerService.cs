namespace Witter.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IFollowerService
    {
        Task Follow(string senderId, string reviverId);

        Task UnFollow(string senderId, string reviverId);

        bool IsFollowing(string senderId, string reviverId);
    }
}
