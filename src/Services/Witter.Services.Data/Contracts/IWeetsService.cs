﻿namespace Witter.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Witter.Data.Models;
    using Witter.Web.ViewModels.Weets;

    public interface IWeetsService
    {
        Task Create(WeetCreateModel model, ApplicationUser user);

        Task Delete(string id);

        void Update(string id);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllByTagId<T>(string id);

        IEnumerable<FullWeetViewModel> Feed(string userId);

        IEnumerable<FullWeetViewModel> Explore();

        T GetByIdToViewModel<T>(string id);

        Task<Weet> GetByIdAsync(string id);
    }
}
