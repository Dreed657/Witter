using System;
using AutoMapper;
using Witter.Common;
using Witter.Data.Models;
using Witter.Services.Mapping;

namespace Witter.Web.ViewModels.Weets
{
    public class FullWeetViewModel : IMapFrom<Weet>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public ApplicationUser Author { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public string CreatedOnOffset { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Weet, FullWeetViewModel>().ForMember(
                x => x.CreatedOnOffset,
                opt =>
                    opt.MapFrom(y => GlobalConstants.TimeConverter(y.CreatedOn))
            );
        }
    }
}
