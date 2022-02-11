using AutoMapper;
using GetConnection.Core.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetConnection.Application.UseCases.Users.ValidateJwtToken
{
    public class ValidateTokenUseCase : IRequest<ValidateTokenResponse>
    {
        public string Token { get; set; }
    }
    public class Handler : IRequestHandler<ValidateTokenUseCase, ValidateTokenResponse>
    {

        private readonly IMapper _mapper;
        private readonly ValidateJwt _validateJwt;
        public Handler(IMapper mapper, ValidateJwt validateJwt)
        {
            _validateJwt = validateJwt;
            _mapper = mapper;

        }
        public async Task<ValidateTokenResponse> Handle(ValidateTokenUseCase request, CancellationToken cancellationToken)
        {
           
            var res = _validateJwt.validate(request.Token);
            ValidateTokenResponse resp = new ValidateTokenResponse();

            if (res == true)
            {
                resp.Status_code = 200;
                resp.Error ="";
                resp.Status = "Token is valid";

            }
            else
            {
                resp.Status_code = 422;
                resp.Error = "Token is Invalid";
                resp.Status = "User Not Found";
            }

            return await Task.FromResult(resp);
        }
    }
}
