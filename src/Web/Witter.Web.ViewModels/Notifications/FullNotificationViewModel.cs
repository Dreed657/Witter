using AutoMapper;
using Witter.Data.Models;
using Witter.Data.Models.Enums;
using Witter.Services.Mapping;

namespace Witter.Web.ViewModels.Notifications
{
    public class FullNotificationViewModel : IMapFrom<Notification>, IHaveCustomMappings
    {
        public NotificationType Type { get; set; }

        public string SenderUsername { get; set; }

        public string RevicerUsername { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Notification, FullNotificationViewModel>()
                .ForMember(x => x.SenderUsername, y => y.MapFrom(z => z.Sender.UserName));

            configuration.CreateMap<Notification, FullNotificationViewModel>()
                .ForMember(x => x.RevicerUsername, y => y.MapFrom(z => z.Revicer.UserName));
        }
    }
}
