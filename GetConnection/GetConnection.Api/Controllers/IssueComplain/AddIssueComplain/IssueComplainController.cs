using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.IssueComplains.AddHrIssue;
using GetConnection.Application.UseCases.IssueComplains.AddIssueComplain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.IssueComplain.AddIssueComplain
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueComplainController : BaseController

    {

        private readonly ILogger<IssueComplainController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public IssueComplainController(ILogger<IssueComplainController> logger, IMapper mapper, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;

        }
      // [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComplain([FromForm]   AddIssueComplainRequest AddIssueComplainRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            AddIssueComplainRequest.EmployeeId = Int64.Parse(userId);
            AddIssueComplainRequest.StatusType = 0;

            var response = await Mediator.Send(new AddIssueComplainUseCase
            {
               AddIssueComplainRequest= AddIssueComplainRequest

            }).ConfigureAwait(false);
            if (response.Status_code ==201)
            {
                return new CreatedResult("",response);
            }
            else if(response.Status_code == 404)
            {
                return NotFound(response);

            }
            else
            {
                return UnprocessableEntity(response);

            }


        }
    }
}
