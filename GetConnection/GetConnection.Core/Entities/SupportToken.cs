using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Entities
{
   public  class SupportToken
    {
        public long Id { get; set; }

        public long IssueId { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }


        public int Type { get; set; }

        public long DashBoardUserId  { get; set; }

        //public User User { get; set; }


        public string Attachment1Path { get; set; }

        public string Attachment2Path { get; set; }

        public string Attachment3Path { get; set; }
    }
}
