using GetConnection.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Services
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(Email email);
    }
}
