using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Entities
{
    public class IssueType
    {
        public int Id  { get; set; }

        public int DeparmentId { get; set; }

        public string TypeName { get; set; }
    }
}
