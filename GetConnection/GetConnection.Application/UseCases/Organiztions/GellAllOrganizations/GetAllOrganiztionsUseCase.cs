using AutoMapper;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Organiztions;
using GetConnection.Core.Services;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Organiztions.GellAllOrganizations
{
    public class GetAllOrganiztionsUseCase : IRequest<IEnumerable<GetAllOrganiztionsResponse>>
    {
      
    }
    public class Handler : IRequestHandler<GetAllOrganiztionsUseCase, IEnumerable<GetAllOrganiztionsResponse>>
    {
        private readonly IOrganiztionReadOnlyRepository _organiztionReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IPushNotificationLogic _pushNotificationLogic;

        public Handler(IPushNotificationLogic pushNotificationLogic, IOrganiztionReadOnlyRepository organiztionReadOnlyRepository , IMapper mapper)
        {
            _organiztionReadOnlyRepository  = organiztionReadOnlyRepository;
            _mapper = mapper;
            _pushNotificationLogic = pushNotificationLogic;


        }
        public async Task<IEnumerable<GetAllOrganiztionsResponse>> Handle(GetAllOrganiztionsUseCase request, CancellationToken cancellationToken)
        {
            GetAllOrganiztionsResponse x = new GetAllOrganiztionsResponse();
        
            //  var y[] = ["aaaaddddddd444444"];

            //  this._pushNotificationLogic.SendPushNotification(y,"nnn","bbbbb",x);

            var orgn  = await _organiztionReadOnlyRepository.GetAllOrganiztion();

            var orgnResponse = orgn.Select(c => _mapper.Map<GetAllOrganiztionsResponse>(c));
            return await Task.FromResult(orgnResponse);
        }
    }
}