namespace Witter.Data.Models
{
    using System;

    using Witter.Data.Common.Models;

    public class WeetTag : BaseModel<string>
    {
        public WeetTag()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string TagId { get; set; }

        public Tag Tag { get; set; }

        public string WeetId { get; set; }

        public Weet Weet { get; set; }
    }
}
