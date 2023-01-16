using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.IService
{
    public interface IFileUpload
    {

        Task UploadAsync(MemoryStream file, string fileName, string folder="images");

    }
}
