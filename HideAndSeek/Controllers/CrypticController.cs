using HideAndSeek.Models;
using HideAndSeek.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HideAndSeek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrypticController : ControllerBase
    {

        private readonly ILogger<CrypticController> _logger;

        public CrypticController(ILogger<CrypticController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ContentResult Get()
        {
            string text ="";
            string name ="";
            string path ="";
            var files = Directory.GetFiles("C:/Users/bloo/source/repos/HideAndSeek/HideAndSeek/Files/");
                if(files.Length == 0)
            {
                return Content("");
            }
            else
            {
                foreach (string strFile in Directory.GetFiles("C:/Users/bloo/source/repos/HideAndSeek/HideAndSeek/Files/"))
                {
                    FileInfo fi = new FileInfo(strFile);
                    name = fi.Name;
                }
                path = "C:/Users/bloo/source/repos/HideAndSeek/HideAndSeek/Files/" + name;
                text = System.IO.File.ReadAllText(path);
            }
            return Content(text);
        }

        [HttpPost]
        [Route("Upload")]
        public IActionResult Upload(IFormFile file, string encryption)

        {
            var encr = encryption;
            var key = HttpContext.Request.Form["key"];
            var action = HttpContext.Request.Form["action"];

            if(action == "encrypt")
            {
                var ms = new MemoryStream();

                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                var encryptedFIleBytes = EncryptBytes.encryptDecrypt(fileBytes, key);
                var path = "C:/Users/bloo/source/repos/HideAndSeek/HideAndSeek/Files/" + file.FileName;
                bool isActionSuccsessful = FileToBytes.ToFile(path, encryptedFIleBytes,fileBytes);
            }
            else
            {
                var ms = new MemoryStream();

                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                var encryptedFIleBytes = EncryptBytes.encryptDecrypt(fileBytes, key);
                var path = "C:/Users/bloo/source/repos/HideAndSeek/HideAndSeek/Files/" + file.FileName;
                bool isActionSuccsessful = FileToBytes.ToFile(path, encryptedFIleBytes);
            }




            return Ok();

        }



        [HttpGet]
        [Route("Download")]
        public HttpResponseMessage Getfile()
        {
            string name = "";
            int fileSize;
            foreach (string strFile in Directory.GetFiles("C:/Users/bloo/source/repos/HideAndSeek/HideAndSeek/Files/"))
            {
                FileInfo fi = new FileInfo(strFile);
                name = fi.Name;
            }

            string localFilePath = "C:/Users/bloo/source/repos/HideAndSeek/HideAndSeek/Files/" + name;

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = name;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            return response;
        }


    }
}
