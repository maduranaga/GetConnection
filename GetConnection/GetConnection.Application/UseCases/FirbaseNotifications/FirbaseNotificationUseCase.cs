using AutoMapper;
using GetConnection.Core.Repositories.FirbaseNotifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.FirbaseNotifications
{
    public class FirbaseNotificationUseCase : IRequest<IEnumerable<FirbaseNotificationResponse>>
    {

        public long UserId { get; set; }

    }
    public class Handler : IRequestHandler<FirbaseNotificationUseCase, IEnumerable<FirbaseNotificationResponse>>
    {
        private readonly IFirbaseNotificationReadOnlyRepository _firbaseNotificationReadOnlyRepository;
        private readonly IMapper _mapper;
  

        public Handler( IFirbaseNotificationReadOnlyRepository organiztionReadOnlyRepository, IMapper mapper)
        {
            _firbaseNotificationReadOnlyRepository = organiztionReadOnlyRepository;
            _mapper = mapper;
  


        }
        public async Task<IEnumerable<FirbaseNotificationResponse>> Handle(FirbaseNotificationUseCase request, CancellationToken cancellationToken)
        {
            FirbaseNotificationResponse x = new FirbaseNotificationResponse();



            var orgn = await _firbaseNotificationReadOnlyRepository.GetNotifications(request.UserId);

            var orgnResponse = orgn.Select(c => _mapper.Map<FirbaseNotificationResponse>(c));
            return await Task.FromResult(orgnResponse);
        }
    }
}