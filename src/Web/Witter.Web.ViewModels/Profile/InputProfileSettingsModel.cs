namespace Witter.Web.ViewModels.Profile
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;

    using Witter.Data.Models;
    using Witter.Services.Mapping;

    public class InputProfileSettingsModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Firstname")]
        public string FirstName { get; set; }

        [Display(Name = "Lastname")]
        public string LastName { get; set; }

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Discription")]
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }

        [Display(Name = "Profile Photo")]
        [DataType(DataType.Upload)]
        public IFormFile ProfileImageSource { get; set; }

        [Display(Name = "Cover")]
        [DataType(DataType.Upload)]
        public IFormFile CoverImageSource { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
