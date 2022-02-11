using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.Users.OtpCodeGenrate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.Users.OtpCodeSend
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
        [Route("generate_token")]
        public async Task<IActionResult> AuthUser(OptCodeGenrateRequest data  )
        {


            var response = await Mediator.Send(new OtpCodeGenrateUseCase
            {
                Email = data.Email

            }).ConfigureAwait(false);
            if(response.Status_code ==200)
            {
                return Ok(response);
            }
            else
            {
                return UnprocessableEntity(response);
            }
         


        }
    }
}