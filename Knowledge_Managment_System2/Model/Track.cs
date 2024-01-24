using Knowledge_Managment_System2.Model;
using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model
{
    public class Track
    {
        [Key]
        public int TrackId { get; set; }

        public string? TrackName { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string? RequiredSkills { get; set; }

        public DateTime Created { get; set; }

        public List<Record> Records { get; set; }
        public List<Course> Courses { get; set; }
    }
}