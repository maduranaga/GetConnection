using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetConnection.Core.Services
{
   
    

        public interface IBlobService
        {
            public Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName);

        }
    
}
