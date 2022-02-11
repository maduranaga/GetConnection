using GetConnection.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.FirbaseNotifications
{
   public class FirbaseNotificationResponse
    {
        public int Status_code { get; set; }
        public string Status { get; set; }
        public FirbaseNotification Data { get; set; }
        public String Error { get; set; }
    }
}
