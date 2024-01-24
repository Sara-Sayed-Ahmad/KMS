using Knowledge_Managment_System2.Model;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Knowledge_Managment_System2.Model
{
    [Table("File")]
    public class FileR
    {
        [Key]
        public string FileName { get; set; }

        public int RecordId { get; set; }
        public Record Record { get; set; }
    }
}
