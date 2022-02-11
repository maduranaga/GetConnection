using GetConnection.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Repositories.FirbaseNotifications
{
   public  interface IFirbaseNotificationReadOnlyRepository
    {
        public Task<List<FirbaseNotification>> GetNotifications(long userId);
    }
}
