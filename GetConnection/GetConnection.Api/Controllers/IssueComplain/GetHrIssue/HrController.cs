using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.IssueComplains.GetHrIssue;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.IssueComplain.GetHrIssue
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

      //  [Authorize]
        [HttpGet]
 
        public async Task<IActionResult> GetHrComplainFilter ([FromQuery(Name = "status")] string[] status ,int limit, int page, string sort, int order, string query)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userNamea = User.FindFirstValue(ClaimTypes.Name);// will give the user's userName


            var response = await Mediator.Send(new IssueComplainUseCase 
            {
                Limit=limit,
                Page=page,
                Sort=sort,
                Status= status,
                Order=order,
                Query=query,
                Type=0,
                UserID = int.Parse(userId)

        }).ConfigureAwait(false);

            return Ok(response);


        }
    }
}
