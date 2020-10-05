using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
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

        public string CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Weet, FullWeetViewModel>().ForMember(
                x => x.CreatedOn,
                opt =>
                    opt.MapFrom(y => ConvertTime(y.CreatedOn))
            );
        }

        private static string ConvertTime(DateTime time)
        {
            var timeSinceUpload = DateTime.Now - time.AddHours(3);
            string timeString;

            if (timeSinceUpload.TotalSeconds < 10)
            {
                timeString = "Now";
            }
            else if (timeSinceUpload.TotalMinutes < 1)
            {
                timeString = $"{timeSinceUpload.Seconds} seconds ago";
            }
            else if (timeSinceUpload.TotalHours < 1)
            {
                timeString = $"{timeSinceUpload.Minutes} minutes ago";
            }
            else if (timeSinceUpload.TotalDays < 1)
            {
                timeString = $"{timeSinceUpload.Hours} hours ago";
            }
            else
            {
                timeString = $"{timeSinceUpload.Days} days ago";
            }

            return timeString;
        }
    }
}
