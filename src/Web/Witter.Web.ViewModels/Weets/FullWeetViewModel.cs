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

        //TODO: Better way to determin time from datetime object

        private static string ConvertTime(DateTime time)
        {
            var timeSpanUpload = DateTime.Now - time;
            string timeString;

            if (timeSpanUpload.Seconds < 10)
            {
                timeString = "Now";
            }
            else if (timeSpanUpload.Minutes < 1)
            {
                timeString = $"{timeSpanUpload.Seconds} seconds ago";
            }
            else if (timeSpanUpload.Hours < 1)
            {
                timeString = $"{timeSpanUpload.Minutes} minutes ago";
            }
            else if (timeSpanUpload.Days < 1)
            {
                timeString = $"{timeSpanUpload.Hours} hours ago";
            }
            else
            {
                timeString = $"{timeSpanUpload.Days} days ago";
            }

            return timeString;
        }
    }
}
