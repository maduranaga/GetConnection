using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.SuportToken.AddSuportToken
{
   public  class AddSuportTicketResponse
    {
        public int Status_code { get; set; }
        public string Status { get; set; }
        public AddTicket Data { get; set; }
        public String Error { get; set; }
    }

    public class AddTicket 
    {
        public long Id { get; set; }

        public long IssueId { get; set; }

        public string Text { get; set; }
        public DateTime Time { get; set; }

        public int Type { get; set; }
        public string Attachment1Path { get; set; }

        public string Attachment2Path { get; set; }

        public string Attachment3Path { get; set; }

    }

    public class DashBoard
    { 
        public long Id { get; set; }

        public string Name { get; set; }

    }


}
