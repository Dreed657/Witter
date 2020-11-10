namespace Witter.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Witter.Data.Common.Models;
    using Witter.Data.Models.Enums;

    public class Notification : BaseModel<string>
    {
        public Notification()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public NotificationType Type { get; set; }

        [Required]
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        [Required]
        public string RevicerId { get; set; }

        public virtual ApplicationUser Revicer { get; set; }
    }
}
