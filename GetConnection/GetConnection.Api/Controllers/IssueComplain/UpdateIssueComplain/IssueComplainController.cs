using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.IssueComplains.UpdateIssueComplain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.IssueComplain.UpdateIssueComplain
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
        [HttpPut]
        public async Task<IActionResult> Update(UpdateIssueComplainRequest UpdateIssueComplainRequest)
        {


            var response = await Mediator.Send(new UpdateIsueComplainUseCase
            {
                UpdateIssueComplainRequest = UpdateIssueComplainRequest

            }).ConfigureAwait(false);
            if (response.Status_code == 201)
            {
                return new CreatedResult("", response);
            }
            else if (response.Status_code == 404)
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