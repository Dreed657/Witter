namespace Witter.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Witter.Data.Common.Repositories;
    using Witter.Data.Models;
    using Witter.Data.Models.Enums;
    using Witter.Services.Data.Contracts;
    using Witter.Services.Mapping;
    using Witter.Web.ViewModels.Notifications;

    public class NotificationService : INotificationsService
    {
        private readonly IRepository<Notification> notificationRepository;

        public NotificationService(IRepository<Notification> notificationRepo)
        {
            this.notificationRepository = notificationRepo;
        }

        public async Task AddNotificationAsync(ApplicationUser sender, ApplicationUser revicer, NotificationType type)
        {
            if (sender == revicer)
            {
                return;
            }

            var entity = new Notification()
            {
                Revicer = revicer,
                Sender = sender,
                Type = type,
            };

            await this.notificationRepository.AddAsync(entity);
            await this.notificationRepository.SaveChangesAsync();
        }

        public IEnumerable<FullNotificationViewModel> GetAllNotificationByUserId(string userId)
        {
            return this.notificationRepository
                .All()
                .Where(x => x.RevicerId == userId)
                .To<FullNotificationViewModel>()
                .ToList();
        }
    }
}
