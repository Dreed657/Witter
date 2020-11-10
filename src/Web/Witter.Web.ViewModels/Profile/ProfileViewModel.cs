using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Witter.Data.Models;
using Witter.Services.Mapping;
using Witter.Web.ViewModels.Weets;

namespace Witter.Web.ViewModels.Profile
{
    public class ProfileViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public ProfileViewModel()
        {
            this.Weets = new HashSet<FullWeetViewModel>();
            this.Images = new HashSet<Media>();
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string AboutMe { get; set; }

        public string LastName { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingCount { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ProfileImageUrl { get; set; }

        public string CoverImageUrl { get; set; }

        public ICollection<Media> Images { get; set; }

        public ICollection<FullWeetViewModel> Weets { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, ProfileViewModel>()
                .ForMember(x => x.ProfileImageUrl, opt =>
                    opt.MapFrom(y => y.ProfileImage.Url));

            configuration.CreateMap<ApplicationUser, ProfileViewModel>()
                .ForMember(x => x.CoverImageUrl, opt =>
                    opt.MapFrom(y => y.CoverImage.Url));

            configuration.CreateMap<ApplicationUser, ProfileViewModel>()
                .ForMember(x => x.FollowersCount, opt =>
                    opt.MapFrom(y => y.Followers
                        .Where(z => z.RevicerId == y.Id && z.IsFollowing).Count()));

            configuration.CreateMap<ApplicationUser, ProfileViewModel>()
                .ForMember(x => x.FollowingCount, opt =>
                    opt.MapFrom(y => y.Following
                        .Where(z => z.SenderId == y.Id && z.IsFollowing).Count()));
        }
    }
}
