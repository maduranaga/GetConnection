using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.LoginUser
{
    public class LoginUserResponse
    {
        public string Status { get; set; }

        public Datas Data    { get; set; }

        public string Error { get; set; }

        public int Status_code { get; set; }
        public class Datas
        {
            public long Id { get; set; }
            public string Email { get; set; }
            public string AccessToken { get; set; }
        }
    }



}

