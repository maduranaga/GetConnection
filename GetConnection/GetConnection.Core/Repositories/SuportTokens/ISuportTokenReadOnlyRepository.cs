using GetConnection.Core.Entities;
using GetConnection.Core.ResponseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Repositories.SuportTokens  
{
   public  interface ISuportTokenReadOnlyRepository
    {
        public Task<List<SuportTokenGetById>> GetSuportTokenByID(long Id);
    }
}
