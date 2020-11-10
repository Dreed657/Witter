namespace Witter.Services.Data
{
    using CloudinaryDotNet;
    using Microsoft.EntityFrameworkCore;
    using SdvCode.Services.Cloud;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Witter.Data.Common.Repositories;
    using Witter.Data.Models;
    using Witter.Services.Contracts;
    using Witter.Services.Data.Contracts;
    using Witter.Services.Mapping;
    using Witter.Web.ViewModels.Weets;

    public class WeetsService : IWeetsService
    {
        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Weet> _weetRepository;
        private readonly IUserService _userService;
        private readonly ITagsService tagsService;

        public WeetsService(Cloudinary cloudinary, IDeletableEntityRepository<Weet> repository, IUserService userService, ITagsService tagsService)
        {
            this.cloudinary = cloudinary;
            this._weetRepository = repository;
            this._userService = userService;
            this.tagsService = tagsService;
        }

        public async Task Create(WeetCreateModel model, ApplicationUser user)
        {
            var entity = new Weet()
            {
                Id = Guid.NewGuid().ToString(),
                Author = user,
                Content = model.Content,
            };

            if (!string.IsNullOrWhiteSpace(model.Tags))
            {
                entity.Tags = await this.ConvertToTags(entity.Id, model.Tags);
            }

            var imageName = Guid.NewGuid().ToString();
            var imageUrl = await ApplicationCloudinary.UploadImage(this.cloudinary, model.Image, imageName);

            if (!string.IsNullOrEmpty(imageUrl))
            {
                entity.Image = new Media()
                {
                    Url = imageUrl,
                    Name = imageName,
                    Creator = user,
                };

                var tagId = await this.tagsService.GetTagId("Images");
                entity.Tags.Add(new WeetTag() { TagId = tagId, WeetId = entity.Id });
            }

            await this._weetRepository.AddAsync(entity);
            await this._weetRepository.SaveChangesAsync();
        }

        public void Update(string id)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(string id)
        {
            var entity = await this.GetByIdAsync(id);

            if (entity == null)
            {
                return;
            }

            this._weetRepository.Delete(entity);
            await this._weetRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this._weetRepository.All().To<T>().ToList();
        }

        public IEnumerable<T> GetAllByTagId<T>(string name)
        {
            return this._weetRepository.All()
                .Where(x => x.Tags.Any(y => y.Tag.Name == name))
                .To<T>()
                .ToList();
        }

        public IEnumerable<FullWeetViewModel> Explore()
        {
            return this._weetRepository
                .All()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedOn)
                .To<FullWeetViewModel>()
                .ToList();
        }

        public IEnumerable<FullWeetViewModel> Feed(string userId)
        {
            var followingUsersIds = this._userService.GetAllUserFollowing(userId);

            return this._weetRepository
                .All()
                .Where(x => followingUsersIds.Contains(x.Author.Id))
                .OrderByDescending(x => x.CreatedOn)
                .To<FullWeetViewModel>()
                .ToList();
        }

        public T GetByIdToViewModel<T>(string Id)
        {
            return this._weetRepository
                .All()
                .Where(x => x.Id == Id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task<Weet> GetByIdAsync(string id)
        {
            return await this._weetRepository.All().Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<ICollection<WeetTag>> ConvertToTags(string weetId, string tags)
        {
            var tagsList = tags.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            var result = new List<WeetTag>();

            foreach (var tag in tagsList)
            {
                var tagId = await this.tagsService.GetTagId(tag);

                result.Add(new WeetTag() { TagId = tagId, WeetId = weetId });
            }

            return result;
        }
    }
}
