using GetConnection.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.ResetPassword
{
    public class ResetPasswordResponse
    {
        public string Status { get; set; }
        public User User { get; set; }
        public String Error { get; set; }

        public int Status_code { get; set; }
    }
}
