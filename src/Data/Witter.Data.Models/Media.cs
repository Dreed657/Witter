using System;
using Witter.Data.Common.Models;

namespace Witter.Data.Models
{
    public class Media : BaseModel<string>
    {
        public Media()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string Url { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}
