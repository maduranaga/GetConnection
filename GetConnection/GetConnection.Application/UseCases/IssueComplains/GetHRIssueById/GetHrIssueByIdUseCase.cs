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

namespace GetConnection.Application.UseCases.IssueComplains.GetHRIssueById
{
    public class GetHrIssueByIdUseCase : IRequest<GetHrIssueByIdResponse>
    {
        public long Id { get; set; }


    }
    public class Handler : IRequestHandler<GetHrIssueByIdUseCase, GetHrIssueByIdResponse>
    {
        private readonly IIssueComplainReadOnlyRepository _issueComplainReadOnlyRepository;
        private readonly IMapper _mapper;


        public Handler(IIssueComplainReadOnlyRepository issueComplainReadOnlyRepository, IMapper mapper)
        {
            _issueComplainReadOnlyRepository = issueComplainReadOnlyRepository;
            _mapper = mapper;

        }
        public async Task<GetHrIssueByIdResponse> Handle(GetHrIssueByIdUseCase request, CancellationToken cancellationToken)
        {
            List<IssueImage> im = new List<IssueImage>();


            var orgn = await _issueComplainReadOnlyRepository.GetHRissueById(request.Id);
            im= await _issueComplainReadOnlyRepository.getImagesByIssueId(request.Id);
            orgn.IssueImage = im;
            GetHrIssueByIdResponse res = new GetHrIssueByIdResponse();
            if (orgn.Id !=0) {
                res.Data = orgn;
                res.Status = "Success";
                res.Error = "";
                res.Status_code = 200;
                return await Task.FromResult(res);
            }
            else
            {
              
                res.Status_code = 404;
                res.Status = "Record not Found";
                res.Error = "UnSuccess";
                return await Task.FromResult(res);
            }
        }
    }
}