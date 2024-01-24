using Knowledge_Managment_System2.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Knowledge_Managment_System2.Model
{
    public class Record
    {
        [Key]
        public int RecordId { get; set; }

        public string? RecordName { get; set; }

        public bool Status { get; set; }

        public bool Wait { get; set; }

        public string? Description { get; set; }

        public bool Mandantory { get; set; }

        public DateTime Created { get; set; }

        public int TrackId { get; set; }
        public Track Track { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public List<Link> Links { get; set; }

        public List<FileR> FileRs { get; set; }
    }
}