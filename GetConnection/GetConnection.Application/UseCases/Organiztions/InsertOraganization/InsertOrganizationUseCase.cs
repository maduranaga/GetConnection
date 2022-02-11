using AutoMapper;
using GetConnection.Application.UseCases.Organiztions.GellAllOrganizations;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Organiztions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Organiztions.InsertOraganization
{
   public  class InsertOrganizationUseCase : IRequest<bool>
    {
        public InsertOrganizationRequest InsertOrganizationRequest { get; set; }
    }
    public class Handler : IRequestHandler<InsertOrganizationUseCase,bool>
    {
        private readonly IOrganiztionWriteOnlyRepository _organiztionWriteOnlyRepository;
        private readonly IMapper _mapper;


        public Handler(IOrganiztionWriteOnlyRepository organiztionReadOnlyRepository, IMapper mapper)
        {
            _organiztionWriteOnlyRepository = organiztionReadOnlyRepository;
            _mapper = mapper;

        }
        public async Task<bool> Handle(InsertOrganizationUseCase request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<InsertOrganizationRequest, Organiztion>(request.InsertOrganizationRequest);

            var orgn = await _organiztionWriteOnlyRepository.InsertOragnizations(data);

            if (orgn) { return await Task.FromResult(true); }
            else { return await Task.FromResult(false); }
          
           
        }
    }
}