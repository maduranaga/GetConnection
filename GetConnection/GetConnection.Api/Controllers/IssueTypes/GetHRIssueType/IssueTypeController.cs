using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.IssueTypes.GerHRIsuueType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.IssueTypes.GetHRIssueType
{
    [ApiController]
    [Route("api/[controller]")]
    public class IssueTypeController : BaseController

    {
        private readonly ILogger<IssueTypeController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public IssueTypeController(ILogger<IssueTypeController> logger, IMapper mapper, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;

        }

        [Authorize]

        [HttpGet]
        [Route("hrcategory")]
        public async Task<IActionResult> GetOrganiztionsList()
        {


            var response = await Mediator.Send(new GetHrIssueTypeUseCase
            {

            }).ConfigureAwait(false);

            return Ok(response);


        }
    }
}