using AutoMapper;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.UpdateUser
{
    public class UpdateUserUseCase : IRequest<UpdateUserResponse>
    {
        public UpdateUserRequest UpdateUserRequest { get; set; }
    }
    public class Handler : IRequestHandler<UpdateUserUseCase, UpdateUserResponse>
    {
        private readonly IUsersWriteOnlyRepository _usersWriteOnlyRepository;
        private readonly IMapper _mapper;



        public Handler(IUsersWriteOnlyRepository usersWriteOnlyRepository,IMapper mapper)
        {
            _usersWriteOnlyRepository = usersWriteOnlyRepository;
            _mapper = mapper;
        }
        public async Task<UpdateUserResponse> Handle(UpdateUserUseCase request, CancellationToken cancellationToken)
        {

            User res = new User();
              res = await _usersWriteOnlyRepository.updateUser(_mapper.Map<UpdateUserRequest,User>(request.UpdateUserRequest));

            UpdateUserResponse resu = new UpdateUserResponse();

       

            if (res != null)
            {
                resu.Status_code = 200;
                resu.Status = "Update Sucess";
                resu.Data = _mapper.Map<User, UpdateUser>(res);
                resu.Error = "";
                return await Task.FromResult(resu);
            }
            else
            {
                resu.Status = "Update Unsucess";
                resu.Error = null;
                return await Task.FromResult(resu);
            }

        }
    }
}
