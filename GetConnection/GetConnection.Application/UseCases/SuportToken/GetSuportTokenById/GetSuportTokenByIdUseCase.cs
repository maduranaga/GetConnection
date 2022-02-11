using AutoMapper;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.SuportTokens;
using GetConnection.Core.ResponseEntity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.SuportToken.GetSuportTokenById
{
    public class GetSuportTokenByIdUseCase : IRequest<GetSuportTokenByIdResponse>
    {
        public long Id{ get; set; }
    }
    public class Handler : IRequestHandler<GetSuportTokenByIdUseCase,GetSuportTokenByIdResponse>
    {
        private readonly ISuportTokenReadOnlyRepository _suportTokenReadOnlyRepository;
        private readonly IMapper _mapper;


        public Handler(ISuportTokenReadOnlyRepository suportTokenReadOnlyRepository, IMapper mapper)
        {
            _suportTokenReadOnlyRepository = suportTokenReadOnlyRepository;
            _mapper = mapper;

        }
        public async Task<GetSuportTokenByIdResponse> Handle(GetSuportTokenByIdUseCase request, CancellationToken cancellationToken)
        {
          

            var orgn = await _suportTokenReadOnlyRepository.GetSuportTokenByID(request.Id);

            GetSuportTokenByIdResponse res = new GetSuportTokenByIdResponse();




            if (orgn.Count != 0)
            {
                res.Status_code = 200;
                res.Status = "Sucess";
                res.Data = _mapper.Map<List<SuportTokenGetById>,List<Token>>(orgn);
                res.Error = "";
                return await Task.FromResult(res);
            }
            else
            {
                res.Status_code = 404;
                res.Status = "Not Found";
                res.Data = null;
                res.Error = "UnSucess";
                return await Task.FromResult(res);

            }


        }
    }
}