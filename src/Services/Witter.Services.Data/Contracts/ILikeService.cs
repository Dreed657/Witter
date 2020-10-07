namespace Witter.Services.Data.Contracts
{
    public interface ILikeService
    {
        int LikesCount(string weetId);

        void Like(string weetId);

        void DisLike(string weetId);

        bool IsLiked(string weetId);
    }
}
