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

namespace GetConnection.Application.UseCases.Users.LogOutUser
{
  public   class LogoutUserUseCase : IRequest<LogOutUserResponse>
    {
        public string DeviceId { get; set; }
    }
    public class Handler : IRequestHandler<LogoutUserUseCase, LogOutUserResponse>
    {
        private readonly IUsersWriteOnlyRepository _usersWriteOnlyRepository;
        private readonly IMapper _mapper;
        private readonly BearerTokenGenrate _bearerTokenGenrate;
        private readonly IUsersReadOnlyRepository _usersReadOnlyRepository;


        public Handler(IUsersReadOnlyRepository  usersReadOnlyRepository,BearerTokenGenrate bearerTokenGenrate,IUsersWriteOnlyRepository usersWriteOnlyRepository, IMapper mapper)
        {
            _usersWriteOnlyRepository = usersWriteOnlyRepository;
            _mapper = mapper;
            _usersReadOnlyRepository = usersReadOnlyRepository;
        }
        public async Task<LogOutUserResponse> Handle(LogoutUserUseCase request, CancellationToken cancellationToken)
        {
            var avilble = await _usersReadOnlyRepository.getById(23);

          //  var token = _bearerTokenGenrate.deletegenerateJwtToken(avilble);
            var res = await _usersWriteOnlyRepository.RemoveDeviceToken(request.DeviceId);

            LogOutUserResponse resu = new LogOutUserResponse();

            if (res == true)
            {
                resu.Status_code = 200;
                resu.Status = "User LogOut Succesfully";
                resu.Error = null;
                return await Task.FromResult(resu);
            }
            else
            {
                resu.Status = "User LogOut UnSuccesfully";
                resu.Error = "User LogOut UnSuccesfully";
                return await Task.FromResult(resu);
            }

        }
    }
}