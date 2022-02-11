using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Repositories.Organiztions
{
   public  interface IOrganiztionWriteOnlyRepository : IRepository<GetConnection.Core.Entities.Organiztion>
    {
        public Task<bool>InsertOragnizations (Organiztion organization);
    }
}
