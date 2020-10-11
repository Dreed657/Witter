using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Witter.Services.Mapping;

namespace Witter.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    using Witter.Data.Common.Repositories;
    using Witter.Data.Models;
    using Witter.Services.Contracts;
    using Witter.Services.Data.Contracts;
    using Witter.Web.ViewModels.Weets;

    public class WeetsService : IWeetsService
    {
        private readonly IDeletableEntityRepository<Weet> _weetRepository;

        private readonly IUserService _userService;

        public WeetsService(IDeletableEntityRepository<Weet> repository, IUserService userService)
        {
            this._weetRepository = repository;
            this._userService = userService;
        }

        public async Task Create(WeetCreateModel model, ApplicationUser user)
        {
            var dbModel = new Weet()
            {
                Id = Guid.NewGuid().ToString(),
                Author = user,
                Content = model.Content,
            };

            await this._weetRepository.AddAsync(dbModel);
            await this._weetRepository.SaveChangesAsync();
        }

        public void Update(string id)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(string id)
        {
            var entity = await this.GetById(id);

            if (entity == null)
            {
                return;
            }

            this._weetRepository.Delete(entity);
            await this._weetRepository.SaveChangesAsync();
        }

        private async Task<Weet> GetById(string id)
        {
            return this._weetRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id)
                .GetAwaiter()
                .GetResult();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this._weetRepository.All().To<T>().ToList();
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

        public FullWeetViewModel Get(string Id)
        {
            var result = this._weetRepository
                .All()
                .To<FullWeetViewModel>()
                .ToList()
                .FirstOrDefault(x => string.Compare(x.Id.ToString(), Id, StringComparison.OrdinalIgnoreCase) == 0);

            return result;
        }
    }
}
