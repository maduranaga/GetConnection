using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.ResponseEntity
{
    public class SuportTokenGetById
    {
        
            public long IssueId { get; set; }

            public string Text { get; set; }

            public int Type { get; set; }

            public DateTime Time { get; set; }

            public long USerId  { get; set; }

           public string UserName  { get; set; }

           public int USerType { get; set; }

          public string Attachment1Path { get; set; }

          public string Attachment2Path { get; set; }
  
          public string Attachment3Path { get; set; }

    }
}
