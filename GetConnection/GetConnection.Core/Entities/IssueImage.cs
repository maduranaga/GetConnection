using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Entities
{
    public class IssueImage 
    {
        public long Id { get; set; }

        public long IssueId { get; set; }

        public string ImagePath { get; set; }
    }
}
