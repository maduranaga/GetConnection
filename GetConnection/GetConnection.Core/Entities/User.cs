using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Email  { get; set; }
        public string EmployeeName { get; set; }
        public int UserType { get; set; }
        public int OrganizationId { get; set; }
        public string HashToken {get; set; }
        public string SaltKey  { get; set; }
        public bool IsActive { get; set; }
        public string OtpCode { get; set; }

        public string Gender { get; set; }
        public string ProfileImage { get; set; }

        
    }
}
