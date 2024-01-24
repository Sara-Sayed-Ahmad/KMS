using Realms;

namespace Knowledge_Managment_System2.Model.AddDTO
{
    public class AddTrackDTO
    {
        [Required]
        public string TrackName { get; set; }

        [Required]
        public int PositionId { get; set; }

        [Required]
        public string RequiredSkills { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public string EmployeeId { get; set; }
    }
}
