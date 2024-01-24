using Knowledge_Managment_System2.Model.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowledge_Managment_System2.Model.DTOs
{
    public class FileDTO 

    {
        [Key]
        public string FileName { get; set; }

        public int RecordId { get; set; }
    }

}
