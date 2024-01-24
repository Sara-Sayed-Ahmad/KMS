namespace Knowledge_Managment_System2.Model.DTOs
{
    public class AchievementDTO 
 
    {
        public string EmployeeId { get; set; }

        public int CourseId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? AchievDate { get; set; }

        public string? Description { get; set; }
    }
}
