using Knowledge_Managment_System2.Model.AddDTO;
using Knowledge_Managment_System2.Model;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Knowledge_Managment_System2.Model.DTOs;

namespace Knowledge_Managment_System2.Repository
{
    public class FileServices : IFileService
    {
        private readonly SystemDbContext _context;
        private readonly IMapper _mapper;

        public FileServices(SystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<string> UploadFile(AddFilesDTO file)
        {
            string filename = "";

            try
            {

                var extension = file.File.FileName;

                filename = extension;
                //fileUp = DateTime.Now.Ticks.ToString();

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "upload");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "upload", filename);

                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.File.CopyToAsync(stream);
                }

                //var file = 

                var newFile = new List<FileR>
                {
                    new FileR
                    {
                       FileName = filename,
                       RecordId = file.RecordId,
                    }
                    
                };

                _context.FileRs.AddRange(newFile);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }

            return filename;

        }

        public async Task DownloadFile(string filename)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "upload", filename);

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(filepath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await File.ReadAllBytesAsync(filepath);

            // return File(bytes, contentType, Path.GetFileName(filepath));
        }

        public async Task<List<FileDTO>> GetAllFiles(int id)
        {
            var RecordId = await _context.FileRs.Where(r => r.RecordId == id).ToListAsync();

            if (RecordId == null)
            {
                throw new ApplicationException("Files is not found");
            }

            return _mapper.Map<List<FileDTO>>(RecordId);
        }

        //public async Task PostFile(IFormFile fileData, FileType fileType)
        //{
        //    try
        //    {
        //        var file = new FileR()
        //        {
        //            //FileId = 0,
        //            FileName = fileData.FileName,
        //            FileType = fileType,
        //        };

        //        using (var stream = new MemoryStream())
        //        {
        //            fileData.CopyTo(stream);
        //            file.FileData = stream.ToArray();
        //        }

        //        var result = _context.FileRs.Add(file);
        //        await _context.SaveChangesAsync(); 
        //    }

        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public async Task PostMultiFile(List<FileUpload> fileData)
        //{
        //    try
        //    {
        //        foreach(FileUpload file in fileData)
        //        {
        //            var fileR = new FileR()
        //            {
        //                FileId = 0,
        //                FileName = file.FileR.FileName,
        //                FileType = file.FileType,
        //            };

        //            using (var stream = new MemoryStream())
        //            {
        //                file.FileR.CopyTo(stream);
        //                fileR.FileData = stream.ToArray();
        //            }

        //            var result = _context.FileRs.Add(fileR);
        //        }
        //        await _context.SaveChangesAsync();
        //    }

        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public async Task DowloadFileById (int Id)
        //{
        //    try
        //    {
        //        var file = _context.FileRs.Where(x => x.FileId == Id).FirstOrDefaultAsync();

        //        var content = new System.IO.MemoryStream(file.Result.FileData);
        //        var path = Path.Combine(
        //            Directory.GetCurrentDirectory(), "FileDownloaded",
        //            file.Result.FileName
        //            );

        //        await CopyStream(content, path);
        //    }

        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public async Task CopyStream(Stream stream, string downloadPath)
        //{
        //    using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
        //    {
        //        await stream.CopyToAsync(fileStream);
        //    }
        //}
    }
}
