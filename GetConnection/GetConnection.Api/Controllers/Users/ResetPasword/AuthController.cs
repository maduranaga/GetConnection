using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.Users.ResetPassword;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.Users.ResetPasword
{
    [ApiController]
    [Route("api/[controller]/")]
    public class AuthController : BaseController

    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthController(ILogger<AuthController> logger, IMapper mapper, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }
 
        [HttpPost]
        [Route("reset_password")]
        public async Task<IActionResult> AuthUser(ResetPasswordRequest data)
        {


            var response = await Mediator.Send(new ResetPasswordUseCase
            {
                Password=data.NewPassword,
                Email=data.Email,
                OtpCode=data.RestToken

            }).ConfigureAwait(false);

            if(response.Status_code ==201)
            {
                return new CreatedResult("",response);
            }
           else if (response.Status_code == 422)
            {
                return UnprocessableEntity(response);
            }
            else
            {
                return NotFound(response);
            }
        


        }
    }
}
