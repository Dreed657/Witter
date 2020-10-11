namespace Witter.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Witter.Data.Models;

    internal class WeetSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Weets.Any())
            {
                return;
            }

            var random = new Random();
            var user = dbContext.Users.First();

            for (var i = 0; i < 5; i++)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var weet = new Weet()
                {
                    Id = Guid.NewGuid().ToString(),
                    Author = user,
                    Content = new string(
                        Enumerable.Repeat(chars, 200)
                            .Select(s => s[random.Next(s.Length)]).ToArray()),
                };

                await dbContext.Weets.AddAsync(weet);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
