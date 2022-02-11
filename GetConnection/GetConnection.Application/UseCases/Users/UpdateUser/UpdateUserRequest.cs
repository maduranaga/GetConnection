using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.UpdateUser
{
   public class UpdateUserRequest
    {
        public long  Id  { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string ProfileImage { get; set; }
        public IFormFile ImageLoad    { get; set; }

    }
}
