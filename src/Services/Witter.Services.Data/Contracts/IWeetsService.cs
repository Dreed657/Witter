namespace Witter.Services.Contracts
{
    using System.Collections.Generic;

    using Witter.Web.ViewModels.Weets;

    public interface IWeetsService
    {
        DetailedWeetViewModel Create(WeetCreateModel model);

        IEnumerable<T> GetAll<T>();
    }
}
