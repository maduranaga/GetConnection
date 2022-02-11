using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.Users.GetUserById;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.Users.GetUserDetailsByUserID
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController

    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserController(ILogger<UserController> logger, IMapper mapper, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }
      //  [Authorize]
        [HttpGet]
        [Route("UserID")]
        public async Task<IActionResult> AuthUser()
        {
            var UserID  = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await Mediator.Send(new GetUserByIdUseCase
            {
                UserID = int.Parse(UserID)

            }).ConfigureAwait(false);

            if(response.Status_code ==200)
            {
                return Ok(response);

            }
            else {
                return NotFound(response);
            }
           
        }
    }
}