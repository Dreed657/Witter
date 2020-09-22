using System.Linq;
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
        private readonly IRepository<Weet> _weetRepository;

        public WeetsService(IRepository<Weet> repository)
        {
            this._weetRepository = repository;
        }

        public DetailedWeetViewModel Create(WeetCreateModel model)
        {
            //var weet = this._mapper.Map<Weet>(model);
            //this._weetRepository.AddAsync(weet);

            //return this._mapper.Map<DetailedWeetViewModel>(weet);

            return null;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this._weetRepository.All().To<T>().ToList();
        }
    }
}
