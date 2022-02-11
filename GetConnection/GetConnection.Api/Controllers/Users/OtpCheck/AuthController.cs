using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.Users.OtpCheck;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.Users.OtpCheck
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
        [Route("check_token")]
        public async Task<IActionResult> AuthUser(OtpCheckRequest data)
        {


            var response = await Mediator.Send(new OtpCheckUseCase
            {
                Email = data.Email,
                OtpCode = data.Otp

            }).ConfigureAwait(false);

            if(response.Status_code ==200)
            {
                return Ok(response);
            }
            else if (response.Status_code == 422)
            {
                return UnprocessableEntity(response);
            }
            else
            {
                return NoContent();
            }
         


        }

     
    }
}