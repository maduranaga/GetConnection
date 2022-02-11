using GetConnection.Core.Entities;
using GetConnection.Core.ResponseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.IssueComplains.GetHRIssueById
{
   public  class GetHrIssueByIdResponse
    {
        public string Status { get; set; }

        public IssuCplainById  Data { get; set; }

        public int Status_code { get; set; }
        public string Error { get; set; }
    }

   

}
