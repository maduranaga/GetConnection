using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.LogOutUser
{
   public  class LogOutUserResponse 
    {
        public string Status { get; set; }

        public int Status_code { get; set; }
        public string Error { get; set; }
    }
}
