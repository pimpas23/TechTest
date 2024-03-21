using TechTest.Business.Notifier;

namespace TechTest.Business.Services
{
    public interface INotifier
    {
        void Handle(Notification notification);
        List<Notification> GetNotifications();
        bool HasNotification();
    }
}
