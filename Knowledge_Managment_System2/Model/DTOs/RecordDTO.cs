using Knowledge_Managment_System2.Model.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.DTOs
{
    public class RecordDTO 
        //: CreateRecordDTO
    {
        [Key]
        public int RecordId { get; set; }

        public string RecordName { get; set; }

        public bool Status { get; set; }

        public bool Wait { get; set; }

        public string Description { get; set; }

        public bool Mandantory { get; set; }

        public DateTime Created { get; set; }

        public int TrackId { get; set; }

        public int Department { get; set; }

        public string EmployeeId { get; set; }

        public List<LinkDTO> Links { get; set; }

        public List<FileDTO> FileRs { get; set; }
    }
}
