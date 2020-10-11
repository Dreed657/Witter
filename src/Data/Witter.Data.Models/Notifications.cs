using System;
using Witter.Data.Common.Models;

namespace Witter.Data.Models
{
    public class Notifications : BaseModel<string>
    {
        public Notifications()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
