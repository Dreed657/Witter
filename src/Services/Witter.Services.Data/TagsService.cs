﻿namespace Witter.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Witter.Data.Common.Repositories;
    using Witter.Data.Models;
    using Witter.Services.Data.Contracts;

    public class TagsService : ITagsService
    {
        private readonly IRepository<Tag> tagRepo;

        public TagsService(IRepository<Tag> tagRepo)
        {
            this.tagRepo = tagRepo;
        }

        public async Task<string> GetTagId(string name)
        {
            return await this.GetOrCreateTag(name);
        }

        private async Task<string> GetOrCreateTag(string name)
        {
            var entity = this.tagRepo.All()
                .FirstOrDefault(x => x.Name == name);

            if (entity == null)
            {
                entity = new Tag(name);
                await this.tagRepo.AddAsync(entity);
                await this.tagRepo.SaveChangesAsync();
            }

            return entity?.Id;
        }
    }
}
