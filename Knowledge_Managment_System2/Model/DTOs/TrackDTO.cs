using Knowledge_Managment_System2.Model.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.DTOs
{
    public class TrackDTO 

    {
        public int TrackId { get; set; }

        public string TrackName { get; set; }

        public int PositionId { get; set; }

        public string EmployeeId { get; set; }

        public string RequiredSkills { get; set; }

        public DateTime Created { get; set; }

        public List<RecordDTO> Records { get; set; }

        public List<CourseDTO> Courses { get; set; }
    }
}
