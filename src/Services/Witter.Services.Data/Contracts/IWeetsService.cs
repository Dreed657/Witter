﻿using System.Threading.Tasks;
using Witter.Data.Models;

namespace Witter.Services.Contracts
{
    using System.Collections.Generic;

    using Witter.Web.ViewModels.Weets;

    public interface IWeetsService
    {
        Task Create(WeetCreateModel model, ApplicationUser user);

        Task Delete(string id);

        void Update(string id);

        Task Like(string id);

        IEnumerable<T> GetAll<T>();

        IEnumerable<FullWeetViewModel> Feed();

        FullWeetViewModel Get(string Id);
    }
}
