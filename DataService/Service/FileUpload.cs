using DataService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace DataService.Service
{
    public class FileUpload : IFileUpload
    {


        private IHostingEnvironment _environment;
        public FileUpload(IHostingEnvironment environment)
        {
            _environment = environment;
        }




        public async Task UploadAsync(MemoryStream file, string fileName, string folder = "images")
        {
            var uploads = Path.Combine(_environment.WebRootPath, folder, fileName);

            await using FileStream fs = new FileStream(uploads, FileMode.Create, FileAccess.Write);
            file.WriteTo(fs);
        }


    }
}
