using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeWEbAPI.Model;

namespace PracticeWEbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        const string FILE_PATH = @"D:\SamplePicture\";
       
        [HttpPost(Name ="FileToUpload")]
        public IActionResult FileToUpload([FromBody] FileToUpload theFile)
            
        {
            

    var filePathName = FILE_PATH + Path.GetFileNameWithoutExtension(theFile.FileName) + "-" +
    DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") +
    Path.GetExtension(theFile.FileName);
    theFile.FileAsByteArray = Convert.FromBase64String(theFile.FileAsBase64);

            if (theFile.FileAsBase64.Contains(","))
            {
                theFile.FileAsBase64 = theFile.FileAsBase64.Substring(theFile.FileAsBase64.IndexOf(",") + 1);
         }
            using (var fs = new FileStream(filePathName, FileMode.CreateNew))
            {
                fs.Write(theFile.FileAsByteArray, 0, theFile.FileAsByteArray.Length);
            }


            return Ok();
        }

    }
}
