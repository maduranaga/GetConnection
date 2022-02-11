using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Entities
{
    public class Area
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string GeoFence { get; set; }

        public string ExclusionFence { get; set; }

        public int AuthorityId { get; set; }

        public int PoliceStationId { get; set; }

        public int GnDevisionId { get; set; }

        public string NumberFireDeperment { get; set; }
    }
}
