using AutoMapper;
using GetConnection.Application.UseCases.Users.InsertDeviceToken;
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

namespace GetConnection.Application.UseCases.Users.OtpCodeGenrate
{
    public class OtpCodeGenrateUseCase:IRequest<OtpCodeGenrateResponse>
    {
        public string Email{ get; set; }
}
public class Handler : IRequestHandler<OtpCodeGenrateUseCase, OtpCodeGenrateResponse>
{
    private readonly IUsersWriteOnlyRepository _usersWriteOnlyRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly IUsersReadOnlyRepository _usersReadOnlyRepository;
     private readonly GenrateOtp _genrateOtp;


        public Handler(GenrateOtp genrateOtp,IUsersReadOnlyRepository usersReadOnlyRepository, IEmailService emailService, IUsersWriteOnlyRepository usersWriteOnlyRepository, HashPassword hashPassword, IMapper mapper)
        {
              _usersWriteOnlyRepository = usersWriteOnlyRepository;
              _mapper = mapper;
              _emailService = emailService;
              _usersReadOnlyRepository = usersReadOnlyRepository;
              _genrateOtp = genrateOtp;

        }
    public async Task<OtpCodeGenrateResponse> Handle(OtpCodeGenrateUseCase request, CancellationToken cancellationToken)
    {
            User user = new User();
            int otpNew =await _genrateOtp.GenerateOtp();
             user = await _usersReadOnlyRepository.getUserByEmail(request.Email);
           
           // user=exitUser;

            OtpCodeGenrateResponse data = new OtpCodeGenrateResponse();

            if (user.Email != null)
            {
                Email email = new Email();
                email.To = request.Email;
                email.Subject = "Get Connection :Your Password Chnage Request";
                email.Content = "Get Connection :Your Password Chnage Request" + " "  + "OTP code"+ "=" + otpNew;
                var send = await _emailService.SendEmail(email);


                var res = _usersWriteOnlyRepository.OtpCodeSave(request.Email, otpNew.ToString());
          

                if (res != null)
                {
                    data.Status_code = 200;
                    data.Status = "Token Generated Successfully";              
                    data.Error = "";
                }
            }
            else
            {
                data.Status_code = 422;
                data.Status = "User not found";
                data.Error = "User not found";
            }
        return await Task.FromResult(data);
    }
}
}