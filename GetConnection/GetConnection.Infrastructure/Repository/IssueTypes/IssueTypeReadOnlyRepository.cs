using GetConnection.Core.Config;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.IssueTypes;
using GetConnection.Core.Services;
using GetConnection.Infrastructure.Context;
using GetConnection.Infrastructure.Repository.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Infrastructure.Repository.IssueTypes
{
    public class IssueTypeReadOnlyRepository : Repository<GetConnection.Core.Entities.IssueType>,IIssueTypeReadOnlyRepository
    {

        private IConfiguration _configuration;
        private GetConnectionContext _getConnection;
        private readonly ISqlHelper _sqlHelper;
        private readonly IOptions<MKConfiguration> _options;
        public IssueTypeReadOnlyRepository(IOptions<MKConfiguration> options, ISqlHelper sqlHelper, 
                                           GetConnectionContext getConnection, GetConnectionContext getConnectionContext, 
                                           IConfiguration configuration) : base(getConnectionContext)
        {
            _getConnection = getConnection;
            _configuration = configuration;
            _options = options;
        }

        public  Task<List<IssueType>> GetHRIssue()
        {
            try
            {
                using (var db = new Context.GetConnectionContext(_configuration))
                {
                    var entityInDb = db.IssueType.Where(r => r.DeparmentId==1).ToList();

                    db.SaveChanges();

                    return Task.FromResult(entityInDb);
                }
            }
            catch (Exception ex)
            {
                List<IssueType> re = new List<IssueType>();
                return Task.FromResult(re);
            }

        }

        public  Task<List<IssueType>> GetMainTenceIssue()
        {
            try
            {
                using (var db = new Context.GetConnectionContext(_configuration))
                {
                    var entityInDb = db.IssueType.Where(r => r.DeparmentId == 2).ToList();

                    db.SaveChanges();

                    return Task.FromResult(entityInDb);
                }
            }
            catch (Exception ex)
            {
                List<IssueType> re = new List<IssueType>();
                return Task.FromResult(re);
            }

        }
    }
}
