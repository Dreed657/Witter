using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Witter.Data.Common.Repositories;
using Witter.Data.Models;
using Witter.Services.Data.Contracts;

namespace Witter.Services.Data
{
    public class LikeService : ILikeService
    {
        private readonly IRepository<WeetLikes> _likesRepository;

        public LikeService(IRepository<WeetLikes> likesRepo)
        {
            this._likesRepository = likesRepo;
        }

        public async Task Like(string userId, string weetId)
        {
            var entity = this._likesRepository
                .All()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.WeetId == weetId)
                .GetAwaiter()
                .GetResult();

            if (entity == null)
            {
                var newEntity = new WeetLikes()
                {
                    UserId = userId,
                    WeetId = weetId,
                    IsLiked = true,
                };

                await this._likesRepository.AddAsync(newEntity);
            }
            else
            {
                entity.IsLiked = true;
            }

            await this._likesRepository.SaveChangesAsync();
        }

        public async Task DisLike(string userId, string weetId)
        {
            var entity = this._likesRepository
                .All()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.WeetId == weetId)
                .GetAwaiter()
                .GetResult();

            entity.IsLiked = false;

            await this._likesRepository.SaveChangesAsync();
        }

        public bool IsLiked(string userId, string weetId)
        {
            var entity = this._likesRepository
                .All()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.WeetId == weetId)
                .GetAwaiter()
                .GetResult();

            return entity?.IsLiked ?? false;
        }
    }
}
