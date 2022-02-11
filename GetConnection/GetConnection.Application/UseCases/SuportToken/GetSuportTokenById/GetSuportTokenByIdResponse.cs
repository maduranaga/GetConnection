using GetConnection.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.SuportToken.GetSuportTokenById
{
    public class GetSuportTokenByIdResponse
    {
        public string Status { get; set; }
        //public List<SupportToken> Data  { get; set; }
        public int Status_code { get; set; }
        public String Error { get; set; }
        public List<Token> Data  { get; set; }
    }

    public class Token
    {
        public int IssueId { get; set; }

        public string Text { get; set; }

        public int Type { get; set; }

        public DateTime Time { get; set; }

        public long USerId { get; set; }

        public string UserName { get; set; }

        public int USerType { get; set; }

        public string Attachment1Path { get; set; }

        public string Attachment2Path { get; set; }

        public string Attachment3Path { get; set; }



    }

    public class DataUser
    {
    public long Id { get; set; }

    public string Name { get; set; }
    }


}
