using GetConnection.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Repositories.SuportTokens 
{
    public interface ISuportTokenWriteOnlyRepository
    {
        public Task<SupportToken> AddSupportToken(SupportToken Token);

        public Task<bool> AddSuportTokenAttachemnt(SuportTokenAttachemnt data);
    }
}
