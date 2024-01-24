using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowledge_Managment_System2.Model.AddDTO
{
    public class AddFilesDTO
    {
        [Key]
        public IFormFile File { get; set; }

        [Required]
        public int RecordId { get; set; }
    }
}
