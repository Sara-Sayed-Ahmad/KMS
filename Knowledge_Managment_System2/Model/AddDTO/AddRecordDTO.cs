using Realms;

namespace Knowledge_Managment_System2.Model.AddDTO
{
    public class AddRecordDTO
    {
        [Required]
        public string RecordName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int TrackId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public string EmployeeId { get; set; }
    }
}
