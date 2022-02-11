using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.InsertDeviceToken
{
   public class InsertDeviceTokenRequest
    {
        public long UserID { get; set; }
        public string UserDeviceToken { get; set; }
        //public DateTime DateTime { get; set; }
    }
}
