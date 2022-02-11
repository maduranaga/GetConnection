using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.SuportToken.AddSuportToken
{
    public class AddSuportTicketRequest
    {
        public long IssueId { get; set; }

        public string Text { get; set; }

        public int Type { get; set; }

        public List<IFormFile> Attachment { get; set; }

        public string Attachment1Path  { get; set; }

        public string Attachment2Path  { get; set; }

        public string Attachment3Path  { get; set; }


    }
}
