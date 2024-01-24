using Knowledge_Managment_System2.Model.AddDTO;
using Knowledge_Managment_System2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Knowledge_Managment_System2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _uploadService;

        public FilesController(IFileService uploadService)
        {
            _uploadService = uploadService;
        }

        ///<summary>
        ///Single File Upload
        ///</summary>
        ///<param name="file"></param>
        ///<returns></returns>
        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile([FromForm] AddFilesDTO file)
        {
            try
            {
                if (file == null)
                {
                    return BadRequest();
                }

                await _uploadService.UploadFile(file);
                return Ok();
            }

            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Download File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpGet("DownloadFile")]
        public async Task<ActionResult> DownloadFile(string filename)
        {
            try
            {
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "upload", filename);

                var provider = new FileExtensionContentTypeProvider();

                if (!provider.TryGetContentType(filepath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(filepath);

                return File(bytes, contentType, Path.GetFileName(filepath));

            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get files by record id
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFiles(int id)
        {
            try
            {
                var files = await _uploadService.GetAllFiles(id);

                if(files != null)
                    return Ok(files);
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}