using MediatR;

namespace DotNETStudy.DDD.MediatR.Messages
{
    public record PersonNotification(string Name, int Age) : INotification
    {
    }
}
