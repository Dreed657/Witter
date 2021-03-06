﻿namespace Witter.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Witter.Data.Common.Repositories;
    using Witter.Data.Models;
    using Witter.Data.Models.Enums;
    using Witter.Services.Contracts;
    using Witter.Services.Data.Contracts;

    public class LikeService : ILikeService
    {
        private readonly IRepository<WeetLikes> likesRepository;
        private readonly IUserService userService;
        private readonly INotificationsService notificationsService;
        private readonly IWeetsService weetService;

        public LikeService(IRepository<WeetLikes> likesRepo, IUserService userService, INotificationsService notificationsService, IWeetsService weetService)
        {
            this.likesRepository = likesRepo;
            this.userService = userService;
            this.notificationsService = notificationsService;
            this.weetService = weetService;
        }

        public async Task Like(string userId, string weetId)
        {
            var entity = this.likesRepository
                .All()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.WeetId == weetId)
                .GetAwaiter()
                .GetResult();

            var user = this.userService.GetUserId(userId);
            var weet = await this.weetService.GetByIdAsync(weetId);

            if (entity == null)
            {
                var newEntity = new WeetLikes()
                {
                    Weet = weet,
                    User = user,
                    IsLiked = true,
                };

                await this.likesRepository.AddAsync(newEntity);
            }
            else
            {
                entity.IsLiked = true;
            }

            await this.notificationsService.AddNotificationAsync(user, weet.Author, NotificationType.Like);
            await this.likesRepository.SaveChangesAsync();
        }

        public async Task DisLike(string userId, string weetId)
        {
            var entity = this.likesRepository
                .All()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.WeetId == weetId)
                .GetAwaiter()
                .GetResult();

            entity.IsLiked = false;

            await this.likesRepository.SaveChangesAsync();
        }

        public bool IsLiked(string userId, string weetId)
        {
            var entity = this.likesRepository
                .All()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.WeetId == weetId)
                .GetAwaiter()
                .GetResult();

            return entity?.IsLiked ?? false;
        }
    }
}
