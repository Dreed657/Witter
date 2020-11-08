using System;
using Witter.Data.Models;
using Witter.Services.Mapping;

namespace Witter.Web.ViewModels.Weets
{
    public class WeetCreateModel : IMapTo<Weet>
    {
        public string Content { get; set; }

        public string Tags { get; set; }
    }
}
