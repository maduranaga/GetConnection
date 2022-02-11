using AutoMapper;
using GetConnection.Core.Repositories.Users;
using GetConnection.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.ResetPassword
{
    public class ResetPasswordUseCase : IRequest<ResetPasswordResponse>
    {
        public string Email { get; set; }
        public string OtpCode { get; set; }

        public string Password { get; set; }
    }
    public class Handler : IRequestHandler<ResetPasswordUseCase, ResetPasswordResponse>
    {
        private readonly IUsersReadOnlyRepository _usersReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly HashPassword _hashPassword;
        private readonly IEmailService _emailService;
        private readonly IUsersWriteOnlyRepository _usersWriteOnlyRepository;



        public Handler(IUsersWriteOnlyRepository usersWriteOnlyRepository,HashPassword hashPassword, IEmailService emailService, IUsersReadOnlyRepository usersReadOnlyRepository, IMapper mapper)
        {
            _usersReadOnlyRepository = usersReadOnlyRepository;
            _mapper = mapper;
            _hashPassword = hashPassword;
            _emailService = emailService;
            _usersWriteOnlyRepository = usersWriteOnlyRepository;
        }
        public async Task<ResetPasswordResponse> Handle(ResetPasswordUseCase request, CancellationToken cancellationToken)
        {
            ResetPasswordResponse data = new ResetPasswordResponse();
            var user = await _usersReadOnlyRepository.getUserByEmail(request.Email);
            if (user.Email !=null)
            {

                var res = await _usersReadOnlyRepository.otpCheck(request.Email, request.OtpCode);        
                if (res != false)
                {

                    var pswd = request.Password;
                    var password = request.Password;
                    var newSalt = _hashPassword.GenerateSalt();

                    var hashedPassword = _hashPassword.ComputeHash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(newSalt));
                    var resp = _usersWriteOnlyRepository.SaveNewPassword(hashedPassword, newSalt, request.Email);

                    data.Status = "Password set successfully";
                    data.Error = null;
                    data.Status_code = 201;
                    return await Task.FromResult(data);
                }
                else
                {
                    data.Status_code = 204;
                    data.Status = "Not Match OTP Code";
                    data.Error = "Not Match OTP Code";
                    return await Task.FromResult(data);
                } }
            else
            {
                data.Status_code = 422;
                data.Status = "User Not Found";
                data.Error = "User Not Found";
                return await Task.FromResult(data);
            }

        }
    }
}