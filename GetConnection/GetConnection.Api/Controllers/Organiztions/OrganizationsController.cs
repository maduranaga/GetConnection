using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.Organiztions.GellAllOrganizations;
using GetConnection.Application.UseCases.Organiztions.InsertOraganization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.Organiztions
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationsController : BaseController

    {
        private readonly ILogger<OrganizationsController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public OrganizationsController(ILogger<OrganizationsController> logger,IMapper mapper,IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;

        }

        
        [HttpGet]
        public async Task<IActionResult> GetOrganiztionsList()
        {


            var response = await Mediator.Send(new GetAllOrganiztionsUseCase
            {
         
            }).ConfigureAwait(false);

            return Ok(response);


        }
        [HttpPost]     
        public async Task<IActionResult> InsertOrganization(InsertOrganizationRequest data)
        {
    

            var response = await Mediator.Send(new InsertOrganizationUseCase 
            {
                InsertOrganizationRequest= data

            }).ConfigureAwait(false);

            return Ok(response);


        }
    }
}