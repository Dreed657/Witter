using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Witter.Services.Mapping;

namespace Witter.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Witter.Data.Common.Repositories;
    using Witter.Data.Models;
    using Witter.Services.Contracts;
    using Witter.Web.ViewModels.Weets;

    public class WeetsService : IWeetsService
    {
        private readonly IDeletableEntityRepository<Weet> _weetRepository;

        public WeetsService(IDeletableEntityRepository<Weet> repository)
        {
            this._weetRepository = repository;
        }

        public async Task Create(WeetCreateModel model, ApplicationUser user)
        {
            var dbModel = new Weet()
            {
                Id = new Guid(),
                Author = user,
                Content = model.Content
            };

            await this._weetRepository.AddAsync(dbModel);
            await this._weetRepository.SaveChangesAsync();
        }

        public void Update(string id)
        {
            throw new NotImplementedException();
        }

        public async Task Like(string id)
        {
            var weet = this.GetById(id);

            weet.Likes++;

            this._weetRepository.Update(weet);
            await this._weetRepository.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            this._weetRepository.Delete(this.GetById(id));
            await this._weetRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this._weetRepository.All().To<T>().ToList();
        }

        public IEnumerable<FeedWeetViewModel> Feed()
        {
            return this._weetRepository
                .All()
                .Where(x => !x.IsDeleted)
                .To<FeedWeetViewModel>()
                .OrderByDescending(x => x.CreatedOn)
                .ToList();
        }

        public DetailedWeetViewModel Get(string Id)
        {
            var result = this._weetRepository.AllAsNoTracking().To<DetailedWeetViewModel>().First(x => x.Id.ToString() == Id);

            return result;
        }

        private Weet GetById(string id)
        {
            return this._weetRepository.All().First(x => x.Id.ToString() == id);
        }
    }
}
