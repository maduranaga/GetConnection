using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.FirbaseNotifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.FirbaseNotifications.GetNotifications
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : BaseController

    {
        private readonly ILogger<NotificationController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public NotificationController(ILogger<NotificationController> logger, IMapper mapper, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;

        }

      //  [Authorize]
        [HttpGet]
      
        public async Task<IActionResult> GetOrganiztionsList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userNamea = User.FindFirstValue(ClaimTypes.Name);// will give the user's userName

            


            var response = await Mediator.Send(new FirbaseNotificationUseCase
            {
                UserId= int.Parse(userId)

        }).ConfigureAwait(false);

            return Ok(response);


        }
    }
}