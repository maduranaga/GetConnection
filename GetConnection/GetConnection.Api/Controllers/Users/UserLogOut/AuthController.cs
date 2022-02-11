using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.Users.LogOutUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.Users.UserLogOut
{
    [ApiController]
    [Route("api/[controller]")]
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

      //  [Authorize]
        [HttpPost]
        [Route("logOut")]

        public async Task<IActionResult> AuthUser(LogoutRequest data)
        {
            // var userId = User.DeleteAsync(ClaimTypes.NameIdentifier);

            var x = await HttpContext.GetTokenAsync("Bearer", "access_token");
         









            var response = await Mediator.Send(new LogoutUserUseCase
            {
                DeviceId = data.DeviceToken

            }).ConfigureAwait(false);

            if (response.Status_code ==200)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }


        }
    }
}