namespace Witter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Witter.Data.Common.Models;

    public class Tag : BaseModel<string>
    {
        public Tag()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Tag(string name)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = name;
        }

        public string Name { get; set; }

        public ICollection<WeetTag> Weets { get; set; }
    }
}
