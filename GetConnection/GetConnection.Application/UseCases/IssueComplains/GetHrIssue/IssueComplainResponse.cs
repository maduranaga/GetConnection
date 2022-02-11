using GetConnection.Core.Entities;
using GetConnection.Core.ResponseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.IssueComplains.GetHrIssue
{
   public class IssueComplainResponse
    {
        public string Status { get; set; }

        public List<IssueComplainFilter> Data { get; set; }

        public int Total { get; set; }

        public int Status_code { get; set; }
        public string Error { get; set; }

        public int Limit { get; set; }

        public int Page { get; set; }


    }
}
