using Knowledge_Managment_System2.Model;
using Knowledge_Managment_System2.Model.AddDTO;
using Knowledge_Managment_System2.Model.DTOs;

namespace Knowledge_Managment_System2.Repository
{
    public interface IFileService
    {
        Task<string> UploadFile(AddFilesDTO file);

        Task DownloadFile(string filename);

        Task<List<FileDTO>> GetAllFiles(int id);
    }
}
