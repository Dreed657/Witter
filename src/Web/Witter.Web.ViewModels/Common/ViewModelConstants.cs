using System;

namespace Witter.Web.ViewModels.Common
{
    public static class ViewModelConstants
    {
        public static string TimeConverter(DateTime time)
        {
            var timeSinceUpload = DateTime.Now - time;
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
