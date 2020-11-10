namespace Witter.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Witter.Data.Models;
    using Witter.Data.Models.Enums;
    using Witter.Web.ViewModels.Notifications;

    public interface INotificationsService
    {
        Task AddNotificationAsync(ApplicationUser sender, ApplicationUser revicer, NotificationType type);

        IEnumerable<FullNotificationViewModel> GetAllNotificationByUserId(string userId);
    }
}
