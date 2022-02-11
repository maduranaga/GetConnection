using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Services
{
   public interface IPushNotificationLogic
    {
        public Task<bool> SendPushNotification(string[] deviceTokens, string title, string body, object data);
    }
}
