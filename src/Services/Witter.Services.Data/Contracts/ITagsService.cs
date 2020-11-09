using System.Collections.Generic;
using System.Threading.Tasks;
using Witter.Web.ViewModels.Weets;

namespace Witter.Services.Data.Contracts
{
    public interface ITagsService
    {
        Task<string> GetTagId(string name);
    }
}
