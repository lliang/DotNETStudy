using DotNETStudy.DDD.MediatR.Messages;
using MediatR;

namespace DotNETStudy.DDD.MediatR.Handlers
{
    public class PersonNotificationHandler : NotificationHandler<PersonNotification>
    {
        protected override void Handle(PersonNotification notification)
        {
            Console.WriteLine($"[PersonNotificationHandler]: {notification}");
        }
    }
}
