using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Entities
{
    public class DeviceToken
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        public string UserDeviceToken { get; set; }

        public DateTime DateTime { get; set; }
    }
}
