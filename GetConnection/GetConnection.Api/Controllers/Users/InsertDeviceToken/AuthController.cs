using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.Users.InsertDeviceToken;
using GetConnection.Core.Entities;
using GetConnection.Core.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.Users.InsertDeviceToken
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


        [Authorize]
        [HttpPost]
        [Route("InsertDeviceToken")]

        public async Task<IActionResult> AuthUser(InsertDeviceTokenRequest dataEnter)
        {


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userNamea = User.FindFirstValue(ClaimTypes.Name);// will give the user's userName

            dataEnter.UserID = int.Parse(userId);


            var response = await Mediator.Send(new InsertDevicetokenUseCase
            {
                InsertDeviceTokenRequest = dataEnter

            }).ConfigureAwait(false);
            if(response.Status_code ==201)
            {
                return new CreatedResult("",response);
            }
            else
            {
                return UnprocessableEntity(response);

            }
          


        }
    }
}