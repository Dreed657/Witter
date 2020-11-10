using System.Threading.Tasks;

namespace Witter.Services.Data.Contracts
{
    public interface ITagsService
    {
        Task<string> GetTagId(string name);
    }
}
