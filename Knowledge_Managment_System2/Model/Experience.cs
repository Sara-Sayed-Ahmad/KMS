namespace Knowledge_Managment_System2.Model
{
    public class Experience
    {
        public int ExperienceId { get; set; }

        public string PositionName { get; set; }

        public int Year { get; set; }

        public string Description { get; set; }

        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}