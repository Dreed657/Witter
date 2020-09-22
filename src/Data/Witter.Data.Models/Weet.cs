using System.ComponentModel.DataAnnotations;

namespace Witter.Data.Models
{
    using System;

    using Witter.Data.Common.Models;

    public class Weet : IAuditInfo
    {
        [Key]
        public Guid Id { get; set; }

        public ApplicationUser Author { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
