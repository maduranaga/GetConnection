using AutoMapper;
using GetConnection.Application.UseCases.Users.GetUserById;
using GetConnection.Core.Repositories.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.OtpCheck
{
    public class OtpCheckUseCase : IRequest<OtpCheckResponse>
    {
        public string Email  { get; set; }
        public string OtpCode  { get; set; }
    }
    public class Handler : IRequestHandler<OtpCheckUseCase, OtpCheckResponse>
    {
        private readonly IUsersReadOnlyRepository _usersReadOnlyRepository;
        private readonly IMapper _mapper;


        public Handler(IUsersReadOnlyRepository usersReadOnlyRepository, IMapper mapper)
        {
            _usersReadOnlyRepository = usersReadOnlyRepository;
            _mapper = mapper;
        }
        public async Task<OtpCheckResponse> Handle(OtpCheckUseCase request, CancellationToken cancellationToken)
        {
            var user = await _usersReadOnlyRepository.getUserByEmail(request.Email);
            OtpCheckResponse data = new OtpCheckResponse();

            if (user.Email !=null)
            {
                var res = await _usersReadOnlyRepository.otpCheck(request.Email, request.OtpCode);
    
                if (res != false)
                {
                    data.Status_code = 200;
                    data.Status = "Token matches.";
                   
                    data.Error = "";
                    return await Task.FromResult(data);
                }
                else
                {
                    data.Status_code = 204;
                    data.Status = "Not Match Otp";
                    data.Error = null;
                    return await Task.FromResult(data);
                }
            }
            else
            {
                data.Status_code =422;
                data.Status = "User Not Found";
                data.Error = null;
                return await Task.FromResult(data);
            }
        }
    }
}