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

namespace GetConnection.Application.UseCases.Users.GetUserById
{
    public class GetUserByIdUseCase : IRequest<GetUserByResponse>
    {
        public long UserID  { get; set; }
    }
    public class Handler : IRequestHandler<GetUserByIdUseCase, GetUserByResponse>
    {
        private readonly IUsersReadOnlyRepository _usersReadOnlyRepository;
        private readonly IMapper _mapper;
     

        public Handler(IUsersReadOnlyRepository usersReadOnlyRepository, IMapper mapper)
        {
            _usersReadOnlyRepository = usersReadOnlyRepository;
            _mapper = mapper;
        }
        public async Task<GetUserByResponse> Handle(GetUserByIdUseCase request, CancellationToken cancellationToken)
        {

            var res = await _usersReadOnlyRepository.getById(request.UserID);
            GetUserByResponse data = new GetUserByResponse();
            if (res !=null)
            {
                data.Status_code = 200;
                data.Status = "Success";
                data.Error="";
                data.User = _mapper.Map<User, UserResponseGetByID>(res);
                return await Task.FromResult(data);
            }
            else
            {
                
                data.Status = "UnSuccess";
                data.Error = "UnSuccess";
                return await Task.FromResult(data);
            }

        }
    }
}