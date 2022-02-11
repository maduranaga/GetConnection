using AutoMapper;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.IssueComplains;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.IssueComplains.UpdateIssueComplain
{
   public class UpdateIsueComplainUseCase : IRequest<UpdateIssueCompalinResponse>
    {

        public UpdateIssueComplainRequest UpdateIssueComplainRequest  { get; set; }

    }
    public class Handler : IRequestHandler<UpdateIsueComplainUseCase, UpdateIssueCompalinResponse>
    {
        private readonly IIssueComplainWriteOnlyRepository _issueComplainWriteOnlyRepository;
        private readonly IMapper _mapper;


        public Handler(IIssueComplainWriteOnlyRepository issueComplainWriteOnlyRepository, IMapper mapper)
        {
            _issueComplainWriteOnlyRepository = issueComplainWriteOnlyRepository;
            _mapper = mapper;

        }
        public async Task<UpdateIssueCompalinResponse> Handle(UpdateIsueComplainUseCase request, CancellationToken cancellationToken)
        {

            var orgnResponse = _mapper.Map<UpdateIssueComplainRequest, IssueComplain>(request.UpdateIssueComplainRequest);

            var orgn = await _issueComplainWriteOnlyRepository.UpdateComplainIssue(orgnResponse);

            UpdateIssueCompalinResponse d = new UpdateIssueCompalinResponse();
            if (orgn == true)
            {
                d.Status = "Success";
                d.Error = null;
                d.Data = null;
                d.Status_code = 201;
                return await Task.FromResult(d);
            }
            else
            {
                d.Status_code = 404;
                d.Status = "Not Found";
                d.Error = "Not Found";
                d.Data = null;
                return await Task.FromResult(d);
            }

        }
    }
}