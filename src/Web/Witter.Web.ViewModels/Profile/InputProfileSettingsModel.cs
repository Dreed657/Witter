namespace Witter.Web.ViewModels.Profile
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Witter.Data.Models;
    using Witter.Services.Mapping;

    public class InputProfileSettingsModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string AboutMe { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
