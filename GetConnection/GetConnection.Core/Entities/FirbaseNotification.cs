using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Entities
{
    public class FirbaseNotification
    {
        public long Id { get; set; }

        public string DeviceToken { get; set; }

        public DateTime DateTime { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }
}
}
