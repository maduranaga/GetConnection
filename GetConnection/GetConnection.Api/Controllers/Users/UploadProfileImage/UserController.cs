using AutoMapper;
using GetConnection.Api.Base;
using GetConnection.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GetConnection.Api.Controllers.Users.UploadProfileImage
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController

    {
        private IBlobService _blobService;

        public UserController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("uploadImage")]
        public async Task<ActionResult> UploadPicture(IFormFile file)
        {
            //IFormFile file = Request.Form.Files[0];
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

            var toReturn = result.AbsoluteUri;

            return Ok(new { path = uniqueFileName });
        }


        /*   string uniqueFileName = null;

           if (files != null)
           {
               string uploadsFolder = Path.Combine("Resources", "Images");
               uniqueFileName = Guid.NewGuid().ToString() + "_" + files.FileName;
               string filePath = Path.Combine(uploadsFolder, uniqueFileName);
               using (var fileStream = new FileStream(filePath, FileMode.Create))
               {
                   files.CopyTo(fileStream);

               }


               string uploadsFolders = Path.Combine(filePath);
               using (FileStream fs = new FileStream(filePath, FileMode.Open))
               {
                   HttpResponseMessage response = new HttpResponseMessage();
                   response.Content = new StreamContent(fs);
                   response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                   using (var memoryStream = new MemoryStream())
                   {
                       fs.CopyTo(memoryStream);
                       Bitmap image = new Bitmap(1, 1);
                       image.Save(memoryStream, ImageFormat.Jpeg);

                       byte[] byteImage = memoryStream.ToArray();

                   }
               }
           }





           return Ok(uniqueFileName);
       }*/


    }
}   
