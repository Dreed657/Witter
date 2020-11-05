﻿using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using Witter.Data.Models;
using Witter.Services.Mapping;
using Witter.Web.ViewModels.Common;
using Witter.Web.ViewModels.Weets;

namespace Witter.Web.ViewModels.Profile
{
    public class ProfileViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public ProfileViewModel()
        {
            this.Weets = new HashSet<FullWeetViewModel>();
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

        public ICollection<FullWeetViewModel> Weets { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
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
