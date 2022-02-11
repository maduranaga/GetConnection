using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Application.UseCases.Users.UpdateUser;
using GetConnection.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.Users.UpdateUser
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController

    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private IBlobService _blobService;


        private readonly IWebHostEnvironment webHostEnvironment;
        public UserController(IBlobService blobService,IWebHostEnvironment hostEnvironment, ILogger<UserController> logger, IMapper mapper, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;
            webHostEnvironment = hostEnvironment;
            _blobService = blobService;
        }
        
     //  [Authorize]
       [HttpPut]

        public async Task<IActionResult> InsertOrganization([FromForm] UpdateUserRequest data)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            data.Id = Int64.Parse(userId);


            IFormFile file = data.ImageLoad;
            if (file == null)
            {
                return BadRequest();
            }
            string uniqueFileName = Guid.NewGuid().ToString() + file.FileName;

            var result = await _blobService.UploadFileBlobAsync(
                         "getconnection",
                         file.OpenReadStream(),
                         file.ContentType,
                         uniqueFileName);

            data.ProfileImage = "https://mankiwwa.blob.core.windows.net/getconnection/" + uniqueFileName;

            var response = await Mediator.Send(new UpdateUserUseCase
            {
                UpdateUserRequest = data

            }).ConfigureAwait(false);

            if(response.Status_code==200)
            {
                return Ok(response);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}