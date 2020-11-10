namespace Witter.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface ILikeService
    {
        Task Like(string userId, string weetId);

        Task DisLike(string userId, string weetId);

        bool IsLiked(string userId, string weetId);
    }
}
