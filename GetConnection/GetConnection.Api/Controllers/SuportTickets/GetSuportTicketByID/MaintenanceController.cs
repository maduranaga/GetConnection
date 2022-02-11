using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.SuportToken.GetSuportTokenById;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.SuportTickets.GetSuportTicketByID
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : BaseController
    {
        private readonly ILogger<MaintenanceController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public MaintenanceController(ILogger<MaintenanceController> logger, IMapper mapper, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;

        }
     //   [Authorize]
        [HttpGet]
        [Route("ticket/{id}")]
        public async Task<IActionResult> GetOrganiztionsList(long id)
        {

            var response = await Mediator.Send(new GetSuportTokenByIdUseCase
            {
                Id = id

            }).ConfigureAwait(false);

            if (response.Status_code == 200)
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