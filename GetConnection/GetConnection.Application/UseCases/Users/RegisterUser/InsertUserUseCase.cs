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

namespace GetConnection.Application.UseCases.Users.RegisterUser
{
   public class InsertUserUseCase : IRequest<InsertUserResponse>
    {
        public InsertUserRequest InsertUserRequest  { get; set; }
    }
    public class Handler : IRequestHandler<InsertUserUseCase, InsertUserResponse>
    {
        private readonly IUsersWriteOnlyRepository _usersWriteOnlyRepository;
        private readonly IMapper _mapper;
        private readonly HashPassword _hashPassword;
        private readonly IEmailService _emailService;


        public Handler(IEmailService emailService, IUsersWriteOnlyRepository usersWriteOnlyRepository , HashPassword hashPassword, IMapper mapper)
        {
            _usersWriteOnlyRepository = usersWriteOnlyRepository;
            _mapper = mapper;
            _hashPassword = hashPassword;
            _emailService =emailService;

        }
        public async Task<InsertUserResponse> Handle(InsertUserUseCase request, CancellationToken cancellationToken)
        {




            var pswd = request.InsertUserRequest.HashToken;
            var password = request.InsertUserRequest.HashToken;
            var newSalt = _hashPassword.GenerateSalt();
            request.InsertUserRequest.SaltKey = newSalt;

            var hashedPassword = _hashPassword.ComputeHash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(newSalt));
            request.InsertUserRequest.HashToken = hashedPassword;
           // request.InsertUserRequest.IsActive = false;

            var data = _mapper.Map<InsertUserRequest, User>(request.InsertUserRequest);

            var orgn = await _usersWriteOnlyRepository.CreateUser(data);

        


            if(orgn==null || orgn.Email==null)
            {
                InsertUserResponse res = new InsertUserResponse();
                res.Status = "UnSuccess";
                res.User =null;
                res.Error = "Cant Be Save";
                return await Task.FromResult(res);

            }
            else
            {
                Email email = new Email();
                email.To = request.InsertUserRequest.Email;
                email.Subject = "Get Connection :Your Account Created";
                email.Content = "Get Connection :Your Account Created  Get Connection Your Password is" + " " + "=" + pswd;
                var send = await _emailService.SendEmail(email);

                InsertUserResponse res = new InsertUserResponse();
                res.Status = "Success";
                res.User = orgn;
                res.Error = null;
                return await Task.FromResult(res);
            }

           

        }
    }
}