using GetConnection.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.IssueTypes.GerHRIsuueType
{
    public class GetHrIssueTypeResponse
    {
        public string Status { get; set; }

        public List<IssueType>  Data  { get; set; }

        public string Error { get; set; }

        public int Status_code { get; set; }
        public class Datas 
        {
            public long Id { get; set; }
            public string Name { get; set; }
  
        }
    }
}
