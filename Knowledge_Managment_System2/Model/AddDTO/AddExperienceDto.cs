using Realms;

namespace Knowledge_Managment_System2.Model.AddDTO
{
    public class AddExperienceDto
    {
        [Required]
        public string PositionName { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string EmployeeId { get; set; }
    }
}
