using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.DTOs
{
    public class ExperienceDTO 

    {
        //public int ExperienceId { get; set; }

        public string PositionName { get; set; }

        public int Year { get; set; }

        public string Description { get; set; }

        public string EmployeeId { get; set; }
    }

}
