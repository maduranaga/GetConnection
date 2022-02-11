using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.Users.ValidateJwtToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.Users.ValidateJwtToken
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

        [HttpGet]
        [Route("{validatetoken}")]
        public async Task<IActionResult> AuthUser(string validatetoken)
        {
         

            var response = await Mediator.Send(new ValidateTokenUseCase
            {
                Token =validatetoken

            }).ConfigureAwait(false);

            if(response.Status_code==200)
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