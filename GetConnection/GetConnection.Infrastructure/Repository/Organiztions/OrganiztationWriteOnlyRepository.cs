using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Organiztions;
using GetConnection.Infrastructure.Context;
using GetConnection.Infrastructure.Repository.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Infrastructure.Repository.Organiztions
{
    public class OrganiztationWriteOnlyRepository : Repository<GetConnection.Core.Entities.Organiztion>, IOrganiztionWriteOnlyRepository
    {

        private IConfiguration _configuration;
        public OrganiztationWriteOnlyRepository (GetConnectionContext getConnectionContext, IConfiguration configuration) : base(getConnectionContext)
        {

            _configuration = configuration;
        }

        public Task<bool> InsertOragnizations(Organiztion organization)
        {

            try
            {
                using (var db = new Context.GetConnectionContext(_configuration))
                {                 
                    var entityInDb = db.Organiztion.Add(organization);
                    db.SaveChanges();
                    return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}