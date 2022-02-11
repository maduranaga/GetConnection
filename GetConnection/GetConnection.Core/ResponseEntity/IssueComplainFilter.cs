using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.ResponseEntity
{
   public class IssueComplainFilter
    {
        public long Id { get; set; }
        public int DeparmentId { get; set; }

        public int IssueTypeID { get; set; }

        public long EmployeeId { get; set; }

        public int StatusType { get; set; }

        public string UserDescription { get; set; }

        public DateTime ComplainDateTime { get; set; }

        public long RowNumber { get; set; }
    }
}
