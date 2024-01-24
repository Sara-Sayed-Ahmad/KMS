using Knowledge_Managment_System2.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowledge_Managment_System2.Model
{
    public class Achievement
    {
        //Composied Key: EmployeeId and CourseId:
        public string Id { get; set; }
        public Employee Employee { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? AchievDate { get; set; }

        public string? Description { get; set; }
    }
}
