using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.RegisterUser
{
    public class InsertUserRequest
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string EmployeeName { get; set; }
        public int UserType { get; set; }
        public int OrganizationId { get; set; }
        public string HashToken { get; set; }
        public string SaltKey { get; set; }
        public bool IsActive { get; set; }
        public string OtpCode { get; set; }

        public IFormFile File { get; set; }

        public string ProfileImage { get; set; }

    }
}
