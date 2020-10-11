using System.Threading.Tasks;
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

        IEnumerable<T> GetAll<T>();

        IEnumerable<FullWeetViewModel> Feed(string userId);

        IEnumerable<FullWeetViewModel> Explore();

        FullWeetViewModel Get(string Id);
    }
}
