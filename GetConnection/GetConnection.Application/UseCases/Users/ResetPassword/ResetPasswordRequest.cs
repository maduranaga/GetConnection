using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.ResetPassword
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }

        public string RestToken { get; set; }

        public string NewPassword { get; set; }
    }
}
