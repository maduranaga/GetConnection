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

namespace GetConnection.Api.Controllers.IssueComplain.GetMainTinIssueOption
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

   //     [Authorize]
        [HttpGet]

        public async Task<IActionResult> GetMTComplainFilter([FromQuery(Name = "values")] string[] status, int limit, int page, string sort, int order, string query)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);// will give the user's userId
            var userNamea = User.FindFirstValue(ClaimTypes.Name);// will give the user's userName

            var response = await Mediator.Send(new IssueComplainUseCase
            {
                Limit = limit,
                Page = page,
                Sort = sort,
                Status = status,
                Order = order,
                Query = query,
                Type=1,
                UserID=int.Parse(userId)
                

            }).ConfigureAwait(false);

            return Ok(response);


        }
    }
}
