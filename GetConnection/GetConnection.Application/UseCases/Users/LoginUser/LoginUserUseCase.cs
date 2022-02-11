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

namespace GetConnection.Application.UseCases.Users.LoginUser
{
    public class LoginUserUseCase:IRequest<LoginUserResponse>
    {
        public LoginUserRequest LoginUserRequest  { get; set; }
}
public class Handler : IRequestHandler<LoginUserUseCase, LoginUserResponse>
{
        private readonly IUsersReadOnlyRepository _usersReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly HashPassword _hashPassword;
        private readonly BearerTokenGenrate _bearerTokenGenrate;

        public Handler(IUsersReadOnlyRepository  usersReadOnlyRepository, IMapper mapper, HashPassword hashPassword, BearerTokenGenrate bearerTokenGenrate)
        {
            _usersReadOnlyRepository = usersReadOnlyRepository;
            _mapper = mapper;
            _hashPassword = hashPassword;
            _bearerTokenGenrate = bearerTokenGenrate;
        }
    public async Task<LoginUserResponse> Handle(LoginUserUseCase request, CancellationToken cancellationToken)
    {

            var avilble = await _usersReadOnlyRepository.getUserByEmail(request.LoginUserRequest.Email);
            if (avilble.Email != null)
            {

                var hash = _hashPassword.ComputeHash(Encoding.UTF8.GetBytes(request.LoginUserRequest.Password), Encoding.UTF8.GetBytes(avilble.SaltKey));
                var orgn = await _usersReadOnlyRepository.userLogin(request.LoginUserRequest.Email, hash);


                if (orgn != null)
                {

                    var token = _bearerTokenGenrate.generateJwtToken(avilble);
                    LoginUserResponse res = new LoginUserResponse();
                    res.Status = "Success Login";
                    res.Error= "";
                    res.Status_code = 200;
                    LoginUserResponse.Datas data = new LoginUserResponse.Datas();
                    data.Id = orgn.Id;
                    data.Email = orgn.Email;
                    data.AccessToken = token;

                    res.Data= data;
                    return await Task.FromResult(res);
                }
                else
                {
                    LoginUserResponse.Datas data = new LoginUserResponse.Datas();
                    LoginUserResponse res = new LoginUserResponse();
                    res.Status = "UnSuccess Login";
                    res.Error = "Password Not Match";
                    res.Data = null;
                    res.Status_code = 422;

                    return await Task.FromResult(res);

                }
            }
            else
            {
              
                LoginUserResponse res = new LoginUserResponse();
                res.Status = "UnSuccess Login";
                res.Error= "User Not Found";
                res.Data = null;
                res.Status_code = 422;

                return await Task.FromResult(res);
            }
          
    


    }
}
}