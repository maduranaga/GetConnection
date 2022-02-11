using AutoMapper;
using GetConnection.Core.Repositories.IssueTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.IssueTypes.GerHRIsuueType
{
    public class GetHrIssueTypeUseCase : IRequest<GetHrIssueTypeResponse>
    {

    }
    public class Handler : IRequestHandler<GetHrIssueTypeUseCase, GetHrIssueTypeResponse>
    {
        private readonly IIssueTypeReadOnlyRepository _issueTypeReadOnlyRepository;
        private readonly IMapper _mapper;


        public Handler(IIssueTypeReadOnlyRepository issueTypeReadOnlyRepository, IMapper mapper)
        {
            _issueTypeReadOnlyRepository = issueTypeReadOnlyRepository;
            _mapper = mapper;

        }
        public async Task<GetHrIssueTypeResponse> Handle(GetHrIssueTypeUseCase request, CancellationToken cancellationToken)
        {

            var orgn = await _issueTypeReadOnlyRepository.GetHRIssue();
            GetHrIssueTypeResponse res = new GetHrIssueTypeResponse();
            res.Status_code = 200;
            res.Status = "Success";
            res.Data = orgn;
            
            return await Task.FromResult(res);
        }
    }
}