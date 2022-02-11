using AutoMapper;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Users;
using GetConnection.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.InsertDeviceToken
{
    public class InsertDevicetokenUseCase : IRequest<InsertDeviceTokenResponse>
    {
        public InsertDeviceTokenRequest InsertDeviceTokenRequest { get; set; }
    }
    public class Handler : IRequestHandler<InsertDevicetokenUseCase, InsertDeviceTokenResponse>
    {
        private readonly IUsersWriteOnlyRepository _usersWriteOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;


        public Handler(IEmailService emailService, IUsersWriteOnlyRepository usersWriteOnlyRepository, HashPassword hashPassword, IMapper mapper)
        {
            _usersWriteOnlyRepository = usersWriteOnlyRepository;
            _mapper = mapper;
            _emailService = emailService;

        }
        public async Task<InsertDeviceTokenResponse> Handle(InsertDevicetokenUseCase request, CancellationToken cancellationToken)
        {
            InsertDeviceTokenResponse data = new InsertDeviceTokenResponse();
            if (request.InsertDeviceTokenRequest.UserDeviceToken !=null && request.InsertDeviceTokenRequest.UserDeviceToken !="") {
                var res = _usersWriteOnlyRepository.InsertToken(_mapper.Map<InsertDeviceTokenRequest, DeviceToken>(request.InsertDeviceTokenRequest));
              

                if (res != null)
                {
                    data.Status_code = 201;
                    data.Status = "Token saved successfully";
                    data.Data = null;
                    data.Error = "";
                }
                else
                {
                    data.Status_code =422;
                    data.Status = "Required fields not supplied";
                    data.Data = null;
                    data.Error = "Required fields not supplied";
                }
            }
            else
            {
                data.Status_code = 422;
                data.Status = "Required fields not supplied";
                data.Data = null;
                data.Error = "Required fields not supplied";

            }
            return await Task.FromResult(data);
        }
    }
}