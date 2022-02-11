using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.ValidateJwtToken
{
    public class ValidateTokenResponse
    {
        public string Status { get; set; }

        public Datas  Data  { get; set; }

        public int Status_code { get; set; }
        public string Error { get; set; }

        public class Datas 
        {
        
        }
    }
}
