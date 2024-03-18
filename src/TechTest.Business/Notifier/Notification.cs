using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest.Business.Notifier
{
    public class Notification
    {
        public Notification(string message)
        {
                this.Message = message;
        }

        public string Message { get; }
    }
}
