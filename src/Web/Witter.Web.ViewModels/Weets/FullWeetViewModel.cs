using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Witter.Data.Models;
using Witter.Services.Mapping;
using Witter.Web.ViewModels.Common;
using Witter.Web.ViewModels.Tags;

namespace Witter.Web.ViewModels.Weets
{
    public class FullWeetViewModel : IMapFrom<Weet>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public ApplicationUser Author { get; set; }

        public string Content { get; set; }

        public int LikeCount { get; set; }

        public string CreatedOnOffset => ViewModelConstants.TimeConverter(TimeZoneInfo.ConvertTimeFromUtc(this.CreatedOn, TimeZoneInfo.Local));

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<TagViewModel> Tags { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Weet, FullWeetViewModel>()
              .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(y => y.Image.Url));

            configuration.CreateMap<Weet, FullWeetViewModel>().ForMember(
                x => x.LikeCount, opt => opt.MapFrom(y => y.Likes.Count(b => b.IsLiked)));
        }
    }
}
