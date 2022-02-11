using AutoMapper;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.IssueComplains;
using GetConnection.Core.ResponseEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.IssueComplains.GetHrIssue
{
    public class IssueComplainUseCase : IRequest<IssueComplainResponse>
    {
        public int Limit { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public int Order { get; set; }
        public string[] Status { get; set; }
        public string Query { get; set; }

        public int Type { get; set; }

        public long UserID { get; set; }


    }
    public class Handler : IRequestHandler<IssueComplainUseCase, IssueComplainResponse>
    {
        private readonly IIssueComplainReadOnlyRepository _issueComplainReadOnlyRepository;
        private readonly IMapper _mapper;


        public Handler(IIssueComplainReadOnlyRepository issueComplainReadOnlyRepository, IMapper mapper)
        {
            _issueComplainReadOnlyRepository = issueComplainReadOnlyRepository;
            _mapper = mapper;

        }
        public async Task<IssueComplainResponse>Handle(IssueComplainUseCase request, CancellationToken cancellationToken)
        {

            if(request.Type==1)
            {

                var orgn = await _issueComplainReadOnlyRepository.GetMaintainIssue(request.Limit, request.Page, request.Sort, request.Order, request.Status, request.Query,request.UserID);

                List<IssueComplainFilter> y = new List<IssueComplainFilter>();

                if(request.Page !=0)
                {
                    if (request.Page !=1) {
                        y = (from cust in orgn
                             where cust.RowNumber >=request.Page*request.Limit && cust.RowNumber < Convert.ToInt64(request.Page*request.Limit+request.Limit)
                             select cust).ToList();
                    }
                    else
                    {
                        y = (from cust in orgn
                             where cust.RowNumber > 0 && cust.RowNumber <= Convert.ToInt64(request.Limit)
                             select cust).ToList();
                    }
                   
                }
                else
                {
                    y = orgn;
                }




                IssueComplainResponse res = new IssueComplainResponse();

           
                if (orgn.Count() % request.Limit == 0) { res.Total = orgn.Count() / request.Limit; }
                else { res.Total = (orgn.Count() / request.Limit) + 1; }



                    res.Status_code = 200;
                    res.Status = "Success";
                    res.Error = "";
                    res.Data = y;
                    res.Limit = request.Limit;
                    res.Page = request.Page;
                    return await Task.FromResult(res);
                
               
            }
            else
            {

                var orgn = await _issueComplainReadOnlyRepository.GetHrIssue(request.Limit, request.Page, request.Sort, request.Order, request.Status, request.Query,request.UserID);
                IssueComplainResponse res = new IssueComplainResponse();

                List<IssueComplainFilter> y = new List<IssueComplainFilter>();

                if (request.Page != 0)
                {
                    if (request.Page != 1)
                    {
                        y = (from cust in orgn
                             where cust.RowNumber >= request.Page * request.Limit && cust.RowNumber < Convert.ToInt64(request.Page * request.Limit + request.Limit)
                             select cust).ToList();
                    }
                    else
                    {
                        y = (from cust in orgn
                             where cust.RowNumber > 0 && cust.RowNumber <= Convert.ToInt64(request.Limit)
                             select cust).ToList();
                    }

                }
                else
                {
                    y = orgn;
                }
                if (orgn.Count() % request.Limit == 0) { res.Total = orgn.Count() / request.Limit; }
                else { res.Total = (orgn.Count() / request.Limit) + 1; }


                res.Status_code = 200;
                    res.Status = "Success";
                    res.Error = "";
                    res.Data = y;
                    res.Limit = request.Limit;
                    res.Page = request.Page;
                    return await Task.FromResult(res);
              
            }

          
           
          
        }
    }
}