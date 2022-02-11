using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.SuportToken.AddSuportToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.SuportTickets.AddSuportTicket
{
    [ApiController]
    [Route("api/[controller]")]
    public class HrController : BaseController

    {
        private readonly ILogger<HrController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public HrController(ILogger<HrController> logger, IMapper mapper, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;

        }

       // [Authorize]
        [HttpPost]
        [Route("ticket")]
        public async Task<IActionResult> GetOrganiztionsList([FromForm]AddSuportTicketRequest data )
        {


            var response = await Mediator.Send(new AddSuportTicketUseCase
            {
                AddSuportTicketRequest = data

            }).ConfigureAwait(false);

            if (response.Status_code == 200)
            {
                return Ok(response);
            }
            else if(response.Status_code == 422)
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
