using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Helpers
{
    public class RemoveJwt
    {
        private readonly AuthorizationFilterContext _context;

        public RemoveJwt(AuthorizationFilterContext  context)
            {
            _context = context;
            }

        public bool RevokeToken(string token, string ipAddress)
        {
            

            return true;
        }
    }
}
