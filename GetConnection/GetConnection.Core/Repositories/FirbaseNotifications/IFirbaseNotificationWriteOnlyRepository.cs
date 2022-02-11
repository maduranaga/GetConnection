using GetConnection.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Repositories.FirbaseNotifications
{
    public interface IFirbaseNotificationWriteOnlyRepository
    {
        public Task<FirbaseNotification> AdNotification (FirbaseNotification data);
    }
}
