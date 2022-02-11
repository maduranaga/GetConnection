using GetConnection.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.ResponseEntity
{
    public class IssuCplainById
    {

        public long Id { get; set; }
      
        public string Name { get; set; }

        public int StatusType { get; set; }

        public string UserDescription { get; set; }

        public DateTime ComplainDateTime { get; set; }

        public List<IssueImage> IssueImage { get; set; }
    }
}
