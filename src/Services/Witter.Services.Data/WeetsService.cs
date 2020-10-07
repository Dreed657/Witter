using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task Delete(string id)
        {
            this._weetRepository.Delete(await this.GetById(id));
            await this._weetRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this._weetRepository.All().To<T>().ToList();
        }

        public IEnumerable<FullWeetViewModel> Feed()
        {
            return this._weetRepository
                .All()
                .Where(x => !x.IsDeleted)
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

        private async Task<Weet> GetById(string id)
        {
            return await this._weetRepository.All().FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }
    }
}
