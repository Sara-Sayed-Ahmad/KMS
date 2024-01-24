using Knowledge_Managment_System2.Model;
using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public string RequiredSkills { get; set; }

        public bool Status { get; set; }

        public bool Wait { get; set; }

        public bool Mandantory { get; set; }

        public string Link_course { get; set; }

        public DateTime Created { get; set; }

        public int TrackId { get; set; }
        public Track Track { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public List<Achievement> Achievements { get; set; }
    }
}
