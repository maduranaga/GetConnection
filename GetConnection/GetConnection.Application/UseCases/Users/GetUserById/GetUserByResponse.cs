using GetConnection.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.GetUserById
{
   public  class GetUserByResponse
    {
        public int Status_code { get; set; }
        public string Status { get; set; }
        public UserResponseGetByID  User { get; set; }
        public String Error { get; set; }
    }

    public class UserResponseGetByID 
    {
        public long Id { get; set; }

        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string ProfileImage { get; set; }

    }

}
