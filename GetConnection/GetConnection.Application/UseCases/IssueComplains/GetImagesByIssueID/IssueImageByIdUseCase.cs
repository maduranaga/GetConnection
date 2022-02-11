using AutoMapper;
using GetConnection.Application.UseCases.IssueComplains.GetImagesByIssueID;
using GetConnection.Core.Repositories.IssueComplains;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.IssueComplains.GetImagesByIssueID
{
    public class IssueImageByIdUseCase : IRequest<IssueImageByIdResponse>
    {
        public long Id { get; set; }


    }
    public class Handler : IRequestHandler<IssueImageByIdUseCase, IssueImageByIdResponse>
    {
        private readonly IIssueComplainReadOnlyRepository _issueComplainReadOnlyRepository;
        private readonly IMapper _mapper;


        public Handler(IIssueComplainReadOnlyRepository issueComplainReadOnlyRepository, IMapper mapper)
        {
            _issueComplainReadOnlyRepository = issueComplainReadOnlyRepository;
            _mapper = mapper;

        }
        public async Task<IssueImageByIdResponse> Handle(IssueImageByIdUseCase request, CancellationToken cancellationToken)
        {


            var orgn = await _issueComplainReadOnlyRepository.getImagesByIssueId(request.Id);
            IssueImageByIdResponse res = new IssueImageByIdResponse();
            if (orgn != null)
            {
                res.Data = orgn;
                res.Status = "Success";
                res.Error = "";
                return await Task.FromResult(res);
            }
            else
            {
                res.Data = orgn;
                res.Status = "Record not Found";
                res.Error = "UnSuccess";
                return await Task.FromResult(res);
            }
        }
    }
}