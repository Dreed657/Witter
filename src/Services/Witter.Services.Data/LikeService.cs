﻿using System.Linq;
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

        public int LikesCount(string weetId)
        {
            return this._likesRepository
                .All()
                .Where(x => x.IsLiked)
                .ToListAsync()
                .GetAwaiter()
                .GetResult()
                .Count(x => x.WeetId.ToString() == weetId);
        }

        public async Task Like(string userId, string weetId)
        {
            var entity = this._likesRepository
                .All()
                .FirstOrDefaultAsync(x => x.ParentId == userId && x.WeetId == weetId)
                .GetAwaiter()
                .GetResult();

            if (entity == null)
            {
                var newEntity = new WeetLikes()
                {
                    ParentId = userId,
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
                .FirstOrDefaultAsync(x => x.ParentId == userId && x.WeetId == weetId)
                .GetAwaiter()
                .GetResult();

            entity.IsLiked = false;

            await this._likesRepository.SaveChangesAsync();
        }

        public bool IsLiked(string userId, string weetId)
        {
            var entity = this._likesRepository
                .All()
                .FirstOrDefaultAsync(x => x.ParentId == userId && x.WeetId == weetId)
                .GetAwaiter()
                .GetResult();

            return entity?.IsLiked ?? false;
        }
    }
}
