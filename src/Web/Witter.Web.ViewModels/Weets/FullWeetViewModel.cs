using System;
using System.Linq;
using AutoMapper;
using Witter.Data.Models;
using Witter.Services.Mapping;
using Witter.Web.ViewModels.Common;

namespace Witter.Web.ViewModels.Weets
{
    public class FullWeetViewModel : IMapFrom<Weet>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public ApplicationUser Author { get; set; }

        public string Content { get; set; }

        public int LikeCount { get; set; }

        public string CreatedOnOffset { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Weet, FullWeetViewModel>().ForMember(
                x => x.CreatedOnOffset, opt => opt.MapFrom(y => y.CreatedOn.ToString()));

            configuration.CreateMap<Weet, FullWeetViewModel>().ForMember(
                x => x.LikeCount, opt => opt.MapFrom(y => y.Likes.Count(b => b.IsLiked)));
        }
    }
}
