using System.Threading.Tasks;

namespace Witter.Services.Data.Contracts
{
    public interface ILikeService
    {
        Task Like(string userId, string weetId);

        Task DisLike(string userId, string weetId);

        bool IsLiked(string userId, string weetId);
    }
}
