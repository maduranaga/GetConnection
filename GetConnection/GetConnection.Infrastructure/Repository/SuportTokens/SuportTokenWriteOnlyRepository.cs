using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.SuportTokens;
using GetConnection.Infrastructure.Context;
using GetConnection.Infrastructure.Repository.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Infrastructure.Repository.SuportTokens 
{
   public class SuportTokenWriteOnlyRepository : Repository<GetConnection.Core.Entities.SupportToken>, ISuportTokenWriteOnlyRepository
    {

        private IConfiguration _configuration;
        public SuportTokenWriteOnlyRepository(GetConnectionContext getConnectionContext, IConfiguration configuration) : base(getConnectionContext)
        {

            _configuration = configuration;
        }

        public Task<SupportToken> AddSupportToken(SupportToken data )
        {

            try
            {
                using (var db = new Context.GetConnectionContext(_configuration))
                {
                    data.Time = DateTime.Now;
                    data.DashBoardUserId = 0;
                    var entityInDb = db.SupportToken.Add(data);
                    db.SaveChanges();
                    return Task.FromResult(data);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public Task<bool> AddSuportTokenAttachemnt(SuportTokenAttachemnt  data)
        {

            try
            {
                using (var db = new Context.GetConnectionContext(_configuration))
                {
              
                    var entityInDb = db.SuportTokenAttachemnt.Add(data);
                    db.SaveChanges();
                    return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }

        }
    }
}