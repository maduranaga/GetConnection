using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Repositories.IssueTypes
{
    public interface  IIssueTypeReadOnlyRepository:IRepository<GetConnection.Core.Entities.IssueType>
    {

        public Task<List<IssueType>> GetHRIssue();

        public Task<List<IssueType>> GetMainTenceIssue();
    }
}
