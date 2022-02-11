using GetConnection.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.IssueComplains.AddIssueComplain
{
  public  class AddIssueComplainResponse
    {
        public string Status { get; set; }

        public List<IssueComplain> Data { get; set; }

        public int Status_code { get; set; }
        public string Error { get; set; }
    }
}
