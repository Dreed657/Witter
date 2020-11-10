namespace Witter.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface ITagsService
    {
        Task<string> GetTagId(string name);
    }
}
