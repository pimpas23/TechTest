using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
