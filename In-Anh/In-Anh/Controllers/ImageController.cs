using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace In_Anh.Controllers
{
    public class ImageController : Controller
    {
        // GET: ImageController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ImageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }




        // POST: ImageController/Create
        [HttpPost]
        
        public async Task<ActionResult> CreateAsync(IFormCollection data)
        {
            var type = data["type"];
            var files = data.Files;


                long size = files.Sum(f => f.Length);

                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                   
                        var filePath = "D:\\imgs\\"+ Guid.NewGuid().ToString() + ".png";
                    UploadIamgeToDB(formFile, filePath);


                    }
                }

                // Process uploaded files
                // Don't rely on or trust the FileName property without validation.

                return Ok(new { count = files.Count, size });
            
        }
        private void UploadIamgeToDB(IFormFile image, string fileName)
        {
           
            // filestream
            using (var fileStream = image.OpenReadStream())

            // memory stream
            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                memoryStream.Position = 0; // The position needs to be reset.

                var before = memoryStream.Length;

                ImageOptimizer optimizer = new ImageOptimizer();
                optimizer.LosslessCompress(memoryStream);
                
                var after = memoryStream.Length;

               var img = new MagickImage(memoryStream);
                img.Format = MagickFormat.Png;
                
                img.WriteAsync(new FileInfo(fileName));


                // convert to byte[]
              
            }

            
        }
    }
}
