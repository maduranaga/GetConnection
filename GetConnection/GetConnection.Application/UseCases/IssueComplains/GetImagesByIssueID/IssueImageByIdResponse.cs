using GetConnection.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.IssueComplains.GetImagesByIssueID
{
   public  class IssueImageByIdResponse
    {
        public string Status { get; set; }

        public List<IssueImage> Data { get; set; }

        public int Status_code { get; set; }
        public string Error { get; set; }
    }
}
