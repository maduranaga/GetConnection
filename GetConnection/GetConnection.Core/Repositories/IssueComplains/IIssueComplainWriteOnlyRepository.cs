using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Repositories.IssueComplains
{
    public interface IIssueComplainWriteOnlyRepository:IRepository<GetConnection.Core.Entities.IssueComplain>
    {
        public Task<IssueComplain> AddComplainIssue(IssueComplain data);

        public Task<bool> UpdateComplainIssue(IssueComplain data);

        public Task<bool> AddIssueIamge(IssueImage data);
    }
}
