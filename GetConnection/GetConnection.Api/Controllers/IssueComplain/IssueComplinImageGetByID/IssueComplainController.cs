using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.IssueComplains.GetImagesByIssueID;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.IssueComplain.IssueComplinImageGetByID
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


     //   [Authorize]
        [HttpGet]
        [Route("IssueImages/{id}")]
        public async Task<IActionResult> GetOrganiztionsList(long id)
        {


            var response = await Mediator.Send(new IssueImageByIdUseCase
            {
                Id = id

            }).ConfigureAwait(false);

            if (response.Error == "")
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
