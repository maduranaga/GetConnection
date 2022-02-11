using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.OtpCheck
{
    public class OtpCheckResponse
    {
        public string Status { get; set; }

        public string Error { get; set; }

        public int Status_code { get; set; }


    }
}
