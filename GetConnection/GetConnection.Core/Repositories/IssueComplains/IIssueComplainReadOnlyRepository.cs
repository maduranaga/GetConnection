using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Base;
using GetConnection.Core.ResponseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Repositories.IssueComplains
{
    public interface IIssueComplainReadOnlyRepository: IRepository<GetConnection.Core.Entities.IssueComplain>
    {
        public Task<List<IssueComplainFilter>> GetHrIssue(int limit,int page,string sort,int order,string[] status,string query,long UserId);

        public  Task<List<IssueComplainFilter >> GetMaintainIssue(int limit, int page, string sort, int order, string[] status, string query,long userID);
        public Task<IssuCplainById> GetHRissueById(long id);

        public Task<List<IssueImage>> getImagesByIssueId(long issueId);

        public Task<List<IssueComplainFilter>> GetAllIssueComaplinIssue(int limit, int page, string sort, int order, string[] status, string query, long userID);
    }
}
