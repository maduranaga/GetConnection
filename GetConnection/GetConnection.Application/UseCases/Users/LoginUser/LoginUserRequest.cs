using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.LoginUser
{
    public class LoginUserRequest
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
